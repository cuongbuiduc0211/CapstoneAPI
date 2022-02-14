using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Services.Services
{
    public class AccessoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccessoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNewAccessory(AccessoryItem accessoryItem)
        {
            Accessory existedAccessory = await _unitOfWork.AccessoryRepository.GetFirstOrDefault(
                q => q.Name.ToLower().Equals(accessoryItem.Name.ToLower()));
            if (existedAccessory != null)
            {
                return false;
            }
            else
            {
                Accessory newAccessory = _mapper.Map<Accessory>(accessoryItem);
                newAccessory.Id = Guid.NewGuid().ToString();
                await _unitOfWork.AccessoryRepository.Add(newAccessory);
                await _unitOfWork.SaveAsync();
                return true;
            }
            
        }

        public async Task<IEnumerable<Accessory>> GetAllAccessories()
        {
            return await _unitOfWork.AccessoryRepository.GetAll(
                null, o => o.OrderBy(s => s.Name), "Brand");            
        }

        public async Task<Accessory> GetAccessoryById(string id)
        {
            return await _unitOfWork.AccessoryRepository.GetFirstOrDefault(q => q.Id == id, "Brand");
        }

        public async Task<IEnumerable<Accessory>> GetAccessoriesByName(string accessoryName)
        {
            return await _unitOfWork.AccessoryRepository.GetAll(
                q => q.Name.Contains(accessoryName),
                o => o.OrderBy(s => s.Name), "Brand");
        }
        public async Task<IEnumerable<Accessory>> GetAccessoriesByBrand(string brandName)
        {
            return await _unitOfWork.AccessoryRepository.GetAll(
                q => q.Brand.Name.Equals(brandName), 
                o => o.OrderBy(s => s.Name), "Brand");
        }
        public async Task<bool> UpdateAccessory(string id, AccessoryItem accessoryItem)
        {
            Accessory updated = await _unitOfWork.AccessoryRepository.GetFirstOrDefault(
                q => q.Id == id);
            List<Accessory> existedList = (await _unitOfWork.AccessoryRepository.GetAll(
                q => q.Id != id)).ToList();
            foreach (var item in existedList)
            {
                if (item.Name.ToLower().Equals(accessoryItem.Name.ToLower()))
                {
                    return false;
                }              
            }
            updated = _mapper.Map<AccessoryItem, Accessory>(accessoryItem);
            updated.Id = id;
            _unitOfWork.AccessoryRepository.Update(updated);
            await _unitOfWork.SaveAsync();
            return true;

        }

        public async Task<bool> RemoveAccessory(string id)
        {
            Accessory accessory = await _unitOfWork.AccessoryRepository.GetFirstOrDefault(q => q.Id == id);
            if (accessory != null)
            {
                _unitOfWork.AccessoryRepository.Remove(accessory);
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
