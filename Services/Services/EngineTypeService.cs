using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EngineTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EngineTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateEngineType(string name)
        {
            EngineType existed = await _unitOfWork.EngineTypeRepository.GetFirstOrDefault(
                q => q.Name.ToLower().Equals(name.ToLower()));
            if (existed != null)
            {
                return false;
            }
            else
            {
                EngineType newEngineType = new EngineType();
                newEngineType.Id = Guid.NewGuid().ToString();
                newEngineType.Name = name;
                await _unitOfWork.EngineTypeRepository.Add(newEngineType);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
        public async Task<IEnumerable<EngineType>> GetEngineTypes()
        {
            return await _unitOfWork.EngineTypeRepository.GetAll(null, o => o.OrderBy(s => s.Name));
        }
        public async Task<bool> UpdateEngineType(string id, string name)
        {
            EngineType existed = await _unitOfWork.EngineTypeRepository.GetFirstOrDefault(
                q => q.Name.ToLower().Equals(name.ToLower()));
            EngineType updated = await _unitOfWork.EngineTypeRepository.GetFirstOrDefault(
                q => q.Id == id);

            if (existed != null)
            {
                return false;
            }
            else
            {
                updated.Name = name;                
                _unitOfWork.EngineTypeRepository.Update(updated);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
        public async Task<bool> RemoveEngineType(string id)
        {
            EngineType existed = await _unitOfWork.EngineTypeRepository.GetFirstOrDefault(q => q.Id == id);
            if (existed != null)
            {
                _unitOfWork.EngineTypeRepository.Remove(existed);
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
