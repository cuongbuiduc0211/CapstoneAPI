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
    public class PrizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PrizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreatNewPrize(PrizeItem prizeItem)
        {
            Prize existedPrize = await _unitOfWork.PrizeRepository.GetFirstOrDefault(
                                                    q => q.Name.ToLower().Equals(prizeItem.Name));
            if (existedPrize != null)
            {
                return false;
            }
            else
            {
                Prize newPrize = _mapper.Map<Prize>(prizeItem);
                newPrize.Id = Guid.NewGuid().ToString();
                await _unitOfWork.PrizeRepository.Add(newPrize);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Prize>> GetAllPrizes()
        {
            return await _unitOfWork.PrizeRepository.GetAll(null, o => o.OrderBy(s => s.Name));
        }

        public async Task<Prize> GetPrizeById(string id)
        {
            return await _unitOfWork.PrizeRepository.GetFirstOrDefault(q => q.Id == id);
        }

        public async Task<IEnumerable<Prize>> GetPrizeByName(string name)
        {
            return await _unitOfWork.PrizeRepository.GetAll(
                q => q.Name.Contains(name),
                o => o.OrderBy(s => s.Name));
        }

        public async Task<bool> UpdatePrize(string id, PrizeItem prizeItem)
        {
            Prize existedPrize = await _unitOfWork.PrizeRepository.GetFirstOrDefault(q => q.Id == id);
            if (existedPrize != null)
            {
                existedPrize = _mapper.Map<PrizeItem, Prize>(prizeItem);
                existedPrize.Id = id;
                _unitOfWork.PrizeRepository.Update(existedPrize);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemovePrize(string id)
        {
            Prize prize = await _unitOfWork.PrizeRepository.GetFirstOrDefault(q => q.Id == id);
            if (prize != null)
            {                
                _unitOfWork.PrizeRepository.Remove(prize);
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
