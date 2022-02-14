using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Services.Services
{
    public class CarModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateCarModel(CarItem item)
        {
            CarModel existedModel = await _unitOfWork.CarModelRepository.GetFirstOrDefault(
                q => q.Name.ToLower().Equals(item.Name.ToLower()) && q.BrandId == item.BrandId);
            if (existedModel != null)
            {
                return false;
            }
            else
            {
                CarModel newModel = _mapper.Map<CarModel>(item);
                newModel.Id = Guid.NewGuid().ToString();
                await _unitOfWork.CarModelRepository.Add(newModel);               
                await _unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task<IEnumerable<CarModel>> GetAllCarModels()
        {
            return await _unitOfWork.CarModelRepository.GetAll(null, o => o.OrderBy(s => s.Name), "Brand");
        }
        public async Task<IEnumerable<CarModel>> GetAllCarModelsByBrand(string brandId)
        {
            return await _unitOfWork.CarModelRepository.GetAll(
                q => q.BrandId == brandId, o => o.OrderBy(s => s.Name), "Brand");
        }
        public async Task<bool> UpdateCarModel(string id, CarItem carItem)
        {            
            CarModel updated = await _unitOfWork.CarModelRepository.GetFirstOrDefault(
                q => q.Id == id);
            List<CarModel> existedList = (await _unitOfWork.CarModelRepository.GetAll(
                q => q.Id != id)).ToList();
            foreach (var item in existedList)
            {
                if (item.Name.ToLower().Equals(carItem.Name.ToLower()) && item.BrandId == carItem.BrandId)
                {
                    return false;
                }
            }
            updated = _mapper.Map<CarItem, CarModel>(carItem);
            updated.Id = id;
            _unitOfWork.CarModelRepository.Update(updated);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveCarModel(string id)
        {
            CarModel car = await _unitOfWork.CarModelRepository.GetFirstOrDefault(q => q.Id == id);
            if (car != null)
            {
                _unitOfWork.CarModelRepository.Remove(car);
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
