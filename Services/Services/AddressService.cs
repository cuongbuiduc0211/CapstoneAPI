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
    public class AddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _unitOfWork.CityRepository.GetAll(null, o => o.OrderBy(s => s.Name));
        }

        public async Task<IEnumerable<District>> GetDistricts(string cityId)
        {
            return await _unitOfWork.DistrictRepository.GetAll(
                q => q.CityId == cityId, 
                o => o.OrderBy(s => s.Name));
        }

        public async Task<IEnumerable<Ward>> GetWards(string districtId)
        {
            return await _unitOfWork.WardRepository.GetAll(
                q => q.DistrictId == districtId,
                o => o.OrderBy(s => s.Name));
        }

        
    }
}
