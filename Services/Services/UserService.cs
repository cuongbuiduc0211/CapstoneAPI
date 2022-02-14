using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;        
        private readonly JWTService _jWTService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, JWTService jWTService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTService = jWTService;
        }

        public async Task<User> CheckUserLogin(UserAccount userAccount)
        {
            User loginedUser = await _unitOfWork.UserRepository.GetFirstOrDefault(
                q => q.Email == userAccount.Email && q.RoleId == (int)UserRole.User 
                && q.Status == (int)UserStatus.Active, "Role"); 
            if (loginedUser != null)
            {
                loginedUser.TokenMobile = userAccount.DeviceToken;
                _unitOfWork.UserRepository.Update(loginedUser);
                await _unitOfWork.SaveAsync();
                return loginedUser;
            }
            return null;
        }

        public async Task<bool> RegisterAccount(SelfProfile selfProfile)
        {
            User existed = await _unitOfWork.UserRepository.GetFirstOrDefault(
                q => q.Email == selfProfile.Email, "Role");
            if (existed != null)
            {
                return false;
            }
            else
            {
                User newUser = _mapper.Map<User>(selfProfile);
                newUser.RoleId = (int)UserRole.User;
                newUser.CreatedDate = DateTime.Now;
                newUser.Status = (int)UserStatus.Active;
                newUser.ExchangePost = 0;
                newUser.TokenMobile = selfProfile.DeviceToken;
                await _unitOfWork.UserRepository.Add(newUser);
                await _unitOfWork.SaveAsync();
                return true;
            }           
        }
        public async Task<bool> ChooseInterestedBrand(InterestedBrandItem item)
        {
            if (item.UserInterestedBrands != null)
            {
                foreach (var interested in item.UserInterestedBrands)
                {
                    InterestedBrand existed = await _unitOfWork.InterestedBrandRepository.GetFirstOrDefault(
                        q => q.UserId == item.UserId && q.BrandId == interested.BrandId);
                    if (existed != null)
                    {
                        existed.BrandId = interested.BrandId;
                        existed.Status = (int)interested.Status;
                        _unitOfWork.InterestedBrandRepository.Update(existed);
                        await _unitOfWork.SaveAsync();
                        //50 brand, tick 5 brand 123456 status 1234 active 5 deactive
                    }
                    else
                    {
                        InterestedBrand newInterestedBrand = new InterestedBrand();
                        newInterestedBrand.Id = Guid.NewGuid().ToString();
                        newInterestedBrand.UserId = item.UserId;
                        newInterestedBrand.BrandId = interested.BrandId;
                        newInterestedBrand.Status = (int)interested.Status;
                        await _unitOfWork.InterestedBrandRepository.Add(newInterestedBrand);
                        await _unitOfWork.SaveAsync();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }                     
        }

        public async Task<IEnumerable<InterestedBrand>> GetUserInterestedBrands(int userId)
        {
            return await _unitOfWork.InterestedBrandRepository.GetAll(
                q => q.UserId == userId && q.Status == (int)InterestedBrandStatus.Like,
                o => o.OrderBy(s => s.Brand.Name), "Brand");
        }

        public async Task<User> CheckAdminLogin(AdminAccount adminAccount)
        {
            // kiểm tra username và pass từ client truyền về server có đúng với
            // 1 row nào đó trong db hay ko?
            
            User loginedUser = await _unitOfWork.UserRepository.GetFirstOrDefault(
                q => q.Username == adminAccount.Username && q.Password == adminAccount.Password &&
                (q.RoleId == (int)UserRole.Admin || q.RoleId == (int)UserRole.Manager) &&
                q.Status == (int)UserStatus.Active, "Role");
            if (loginedUser != null)
            {
                loginedUser.TokenWeb = adminAccount.DeviceToken;
                _unitOfWork.UserRepository.Update(loginedUser);
                await _unitOfWork.SaveAsync();
                return loginedUser;
            }
            return null;
        }
        //public async Task<bool> LogOut()
        //{

        //}
        public async Task<bool> CreateNewAccount(NewAccount account)
        {
            User existedAccount = await _unitOfWork.UserRepository.GetFirstOrDefault(
                q=> q.Username.ToLower().Equals(account.Username));
            if (existedAccount != null)
            {
                return false;
            }
            else
            {
                User newAccount = new User();
                newAccount.Username = account.Username;
                newAccount.Password = account.Password;
                newAccount.FullName = account.FullName;
                newAccount.RoleId = (int)account.Role; // int roleId = 1 2 3
                newAccount.CreatedDate = DateTime.Now;
                newAccount.Status = (int)UserStatus.Active;
                await _unitOfWork.UserRepository.Add(newAccount);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }

        
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAll(q => q.RoleId == (int) UserRole.User,
                                                            o => o.OrderBy(s => s.FullName));
        }
        public async Task<IEnumerable<User>> GetAllAdminsAndManagers()
        {
            return await _unitOfWork.UserRepository.GetAll(q => q.RoleId == (int)UserRole.Admin ||
                                     q.RoleId == (int)UserRole.Manager, o => o.OrderBy(s => s.Role.Name));
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.UserRepository.Get(id);
        }

        public async Task<IEnumerable<User>> GetUserByFullName(string fullName)
        {
            return await _unitOfWork.UserRepository.GetAll(q => q.FullName.Contains(fullName),
                                                            o => o.OrderBy(s => s.FullName));
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Email.Contains(email));
        }

        public async Task<bool> UpdateSelfProfile(int id, SelfProfile selfProfile)
        {
            User existedUser = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == id);
            if (existedUser != null)
            {
                existedUser.Email = selfProfile.Email;
                existedUser.FullName = selfProfile.FullName;
                existedUser.Gender = (int)selfProfile.Gender;
                existedUser.YearOfBirth = selfProfile.YearOfBirth;
                existedUser.Image = selfProfile.Image;
                existedUser.Phone = selfProfile.Phone;
                existedUser.Address = selfProfile.Address;
                _unitOfWork.UserRepository.Update(existedUser);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> ChangePassword (int id, ChangePassword changePassword)
        {
            User existedUser = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == id);
            if (existedUser != null)
            {
                if (changePassword.ConfirmedPassword.Equals(changePassword.Password))
                {
                    //existedUser = _mapper.Map<ChangePassword, User>(changePassword);
                    existedUser.Password = changePassword.Password;
                    _unitOfWork.UserRepository.Update(existedUser);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
            
            
        }

        public async Task<bool> ChangeAccountStatus(int id, UserStatus userStatus)
        {
            User user = await _unitOfWork.UserRepository.Get(id);
            if (user != null)
            {
                user.Status = (int) userStatus;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
            
        }    
    }
}
