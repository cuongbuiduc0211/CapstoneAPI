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
    public class GenerationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateGeneration(GenerationItem item)
        {
            Generation exited = await _unitOfWork.GenerationRepository.GetFirstOrDefault(
                q => q.CarModelId == item.CarModelId && q.Name == item.Name &&
                q.YearOfManufactor == item.YearOfManufactor);
            if (exited != null)
            {
                return false;
            }
            else
            {
                Generation newGen = _mapper.Map<Generation>(item);
                newGen.Id = Guid.NewGuid().ToString();
                await _unitOfWork.GenerationRepository.Add(newGen);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
        public async Task<IEnumerable<Generation>> GetAllGenerations()
        {
            return await _unitOfWork.GenerationRepository.GetAll(
                null, o => o.OrderBy(s => s.Name),
                "CarModel");
        }
        public async Task<IEnumerable<Generation>> GetAllGenerationsByBrand(string brandId)
        {
            return await _unitOfWork.GenerationRepository.GetAll(
                q => q.CarModel.BrandId == brandId,
                o => o.OrderBy(s => s.Name),
                "CarModel");
        }
        public async Task<IEnumerable<Generation>> GetAllGenerationsByCarModel(string carModelId)
        {
            return await _unitOfWork.GenerationRepository.GetAll(
                q => q.CarModelId == carModelId,
                o => o.OrderBy(s => s.Name),
                "CarModel");
        }
        
        public async Task<bool> UpdateGeneration(string id, GenerationItem genItem)
        {
            
            Generation updated = await _unitOfWork.GenerationRepository.GetFirstOrDefault(
                q => q.Id == id);
            List<Generation> existedList = (await _unitOfWork.GenerationRepository.GetAll(
                q => q.Id != id)).ToList();
            foreach (var item in existedList)
            {
                if (item.Name.ToLower().Equals(genItem.Name.ToLower()) &&
                    item.CarModelId == genItem.CarModelId && item.YearOfManufactor == genItem.YearOfManufactor)
                {
                    return false;
                }
            }
            updated = _mapper.Map<GenerationItem, Generation>(genItem);
            updated.Id = id;
            _unitOfWork.GenerationRepository.Update(updated);
            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task<bool> RemoveGeneration(string id)
        {
            Generation generation = await _unitOfWork.GenerationRepository.GetFirstOrDefault(q => q.Id == id);
            if (generation != null)
            {
                _unitOfWork.GenerationRepository.Remove(generation);
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
