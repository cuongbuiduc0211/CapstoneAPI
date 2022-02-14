using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace Services.Services
{
    public class BrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        public async Task<bool> CreateNewBrand(BrandItem brandItem)
        {
            
            Brand existed = await _unitOfWork.BrandRepository.GetFirstOrDefault(
                q => q.Name.ToLower().Equals(brandItem.Name.ToLower()));
            if (existed != null)
            {
                return false; 
            }
            else
            {
                Brand newBrand = _mapper.Map<Brand>(brandItem);
                newBrand.Type = (int) brandItem.Type;
                newBrand.Id = Guid.NewGuid().ToString();
                await _unitOfWork.BrandRepository.Add(newBrand);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsOfCar()
        {
            return await _unitOfWork.BrandRepository.GetAll(
                q => q.Type == (int)BrandType.Car,
                o => o.OrderBy(s => s.Name));
        }
        public async Task<IEnumerable<Brand>> GetAllBrandsOfAccessory()
        {
            return await _unitOfWork.BrandRepository.GetAll(
                q => q.Type == (int)BrandType.Accessory, 
                o => o.OrderBy(s => s.Name));
        }
        public async Task<Brand> GetBrandById(string id)
        {
            return await _unitOfWork.BrandRepository.GetFirstOrDefault(q => q.Id == id);
        }

        public async Task<IEnumerable<Brand>> GetBrandByName(string brandName)
        {
            return await _unitOfWork.BrandRepository.GetAll(
                q => q.Name.Contains(brandName),
                o => o.OrderBy(s => s.Name), "Cars, Accessories");
        }

        public async Task<bool> UpdateBrand(string id, BrandItem brandItem)
        {
            
            Brand updated = await _unitOfWork.BrandRepository.GetFirstOrDefault(
                q => q.Id == id);
            List<Brand> existedList = (await _unitOfWork.BrandRepository.GetAll(
                q => q.Id != id)).ToList();
            foreach (var item in existedList)
            {
                if (item.Name.ToLower().Equals(brandItem.Name.ToLower()))
                {
                    return false;
                }
            }
            updated = _mapper.Map<BrandItem, Brand>(brandItem);
            updated.Type = (int)brandItem.Type;
            updated.Id = id;
            _unitOfWork.BrandRepository.Update(updated);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveBrand(string id)
        {
            Brand brand = await _unitOfWork.BrandRepository.GetFirstOrDefault(q => q.Id == id);
            if (brand != null)
            {
                _unitOfWork.BrandRepository.Remove(brand);
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
