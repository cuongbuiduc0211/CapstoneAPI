using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;
using Utility.ViewModels.Report;

namespace Services.Services
{
    public class ExchangeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExchangeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        public async Task<bool> CreateCarExchange(ExchangeCarItem exchangeCarItem)
        {
            User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == exchangeCarItem.UserId);
            if (user.ExchangePost < 100)
            {
                
                var exchangeId = Guid.NewGuid().ToString();
                Exchange exchange = _mapper.Map<Exchange>(exchangeCarItem);
                
                exchange.Id = exchangeId;
                exchange.Type = (int)ExchangeType.Car;
                exchange.CreatedDate = DateTime.Now;
                exchange.Status = (int)ExchangeStatus.InProcess;
                foreach (var detail in exchange.ExchangeCarDetails)
                {
                    exchange.Total += (detail.Price * detail.Amount);
                    detail.Id = Guid.NewGuid().ToString();
                    detail.ExchangeId = exchangeId;
                }
                await _unitOfWork.ExchangeRepository.Add(exchange);
                user.ExchangePost += 1;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> CreateAccessoryExchange(ExchangeAccItem exchangeAccItem)
        {
            User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == exchangeAccItem.UserId);
            if (user.ExchangePost < 100)
            {
                var exchangeId = Guid.NewGuid().ToString();
                Exchange exchange = _mapper.Map<Exchange>(exchangeAccItem);
                exchange.Id = exchangeId;
                exchange.Type = (int)ExchangeType.Accessory;
                exchange.CreatedDate = DateTime.Now;
                exchange.Status = (int)ExchangeStatus.InProcess;
                foreach (var detail in exchange.ExchangeAccessorryDetails)
                {
                    exchange.Total += (detail.Price * detail.Amount);
                    detail.Id = Guid.NewGuid().ToString();
                    detail.ExchangeId = exchangeId;
                }
                await _unitOfWork.ExchangeRepository.Add(exchange);
                user.ExchangePost += 1;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TopBrandViewModel>> GetTopExCarBrandsByMonth(DateTime date)
        {
            var result = new List<TopBrandViewModel>();
            var exchanges = await _unitOfWork.ExchangeRepository.GetAll(
                q => q.CreatedDate.Month == date.Month && q.CreatedDate.Year == date.Year, null, "ExchangeCarDetails");
            if (exchanges != null)
            {
                foreach (var exchange in exchanges)
                {
                    foreach (var exDetail in exchange.ExchangeCarDetails)
                    {
                        var brand = await _unitOfWork.BrandRepository.GetFirstOrDefault(q => q.Id == exDetail.BrandId);
                        TopBrandViewModel existedbrand = result.FirstOrDefault(
                                q => q.BrandName == brand.Name);
                        if (existedbrand != null)
                        {
                            existedbrand.Count++;
                        }
                        else
                        {
                            result.Add(new TopBrandViewModel()
                            {
                                BrandName = brand.Name,
                                Count = 1
                            });
                        }
                    }
                }
            }
            return result;
        }

        public async Task<int> CountExchangesByMonth(ExchangeType type, DateTime date)
        {
            var count = (await _unitOfWork.ExchangeRepository.GetAll(
                q => q.Type == (int)type && q.CreatedDate.Month == date.Month
                && q.CreatedDate.Year == date.Year)).Count();
            return count;
        }
        public async Task<IEnumerable<Exchange>> GetInProcessExchanges(ExchangeType type, int userId)
        {
            return await _unitOfWork.ExchangeRepository.GetAll(
                q => q.Type == (int)type && q.UserId == userId && q.Status == (int)ExchangeStatus.InProcess,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails");
        }
        public async Task<IEnumerable<Exchange>> GetFinishedExchanges(ExchangeType type, int userId)
        {
            return await _unitOfWork.ExchangeRepository.GetAll(
                q => q.Type == (int)type && q.UserId == userId && q.Status == (int)ExchangeStatus.Finished,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails");
        }
        public async Task<IEnumerable<Exchange>> GetAllExchanges(ExchangeType type, int userId)
        {
            return await _unitOfWork.ExchangeRepository.GetAll(
                q => q.UserId != userId && q.Type == (int)type && q.Status == (int)ExchangeStatus.InProcess,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails");
        }
        public async Task<IEnumerable<Exchange>> GetExchangesInCity(ExchangeType type, int userId, string cityId)
        {
            return await _unitOfWork.ExchangeRepository.GetAll(
                q => q.UserId != userId && q.Type == (int)type && 
                q.CityId == cityId && q.Status == (int)ExchangeStatus.InProcess,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails");
        }
        public async Task<IEnumerable<Exchange>> GetExchangesInDistrict(ExchangeType type, int userId, string cityId, string districtId)
        {
            return await _unitOfWork.ExchangeRepository.GetAll(
                q => q.UserId != userId && q.Type == (int)type && q.CityId == cityId && 
                     q.DistrictId == districtId && q.Status == (int)ExchangeStatus.InProcess,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails");
        }
        public async Task<Exchange> GetExchangeById(string id)
        {
            return await _unitOfWork.ExchangeRepository.GetFirstOrDefault(
                q => q.Id.Equals(id),
                "User, City, District, Ward, ExchangeCarDetails, ExchangeAccessorryDetails, ExchangeResponses");
        }

        public async Task<bool> CancelExchange(string id)
        {
            Exchange exchange = await _unitOfWork.ExchangeRepository.GetFirstOrDefault(q => q.Id.Equals(id));
            User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == exchange.UserId);
            if (exchange != null)
            {
                exchange.Status = (int)ExchangeStatus.Canceled;
                _unitOfWork.ExchangeRepository.Update(exchange);
                user.ExchangePost -= 1;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
