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

namespace Services.Services
{
    public class ContestPrizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContestPrizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreatePrizeForContest(ContestPrizeItem item)
        {
            ContestPrize existed = await _unitOfWork.ContestPrizeRepository.GetFirstOrDefault(
                q => q.ContestId == item.ContestId && q.PrizeOrder.ToLower().Equals(item.PrizeOrder));
            if (existed != null)
            {
                return false;
            }
            else
            {
                ContestPrize contestPrize = _mapper.Map<ContestPrize>(item);
                contestPrize.Id = Guid.NewGuid().ToString();
                contestPrize.CreatedDate = DateTime.Now;
                await _unitOfWork.ContestPrizeRepository.Add(contestPrize);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
        public async Task<IEnumerable<ContestPrize>> GetPrizesByContestId(string contestId)
        {
            return await _unitOfWork.ContestPrizeRepository.GetAll(
                q => q.ContestId == contestId,
                o => o.OrderBy(s => s.PrizeOrder),
                "User, Manager, Prize");
        }
        public async Task<IEnumerable<ContestEventRegister>> GetJoinedUsers(string contestEventId)
        {
            return await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == contestEventId && q.Status == (int)UserEventContestStatus.Joined,
                o => o.OrderBy(s => s.RegisterDate),
                "User");
        }

        public async Task<bool> UpdatePrizeForContest(string id, ContestPrizeItem item)
        {
            ContestPrize existed = await _unitOfWork.ContestPrizeRepository.GetFirstOrDefault(
                q => q.Id == id);
            if (existed != null)
            {
                existed = _mapper.Map<ContestPrizeItem, ContestPrize>(item);
                existed.Id = id;
                _unitOfWork.ContestPrizeRepository.Update(existed);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemovePrizeForContest(string id)
        {
            ContestPrize existed = await _unitOfWork.ContestPrizeRepository.GetFirstOrDefault(
                q => q.Id == id);
            if (existed != null)
            {
                _unitOfWork.ContestPrizeRepository.Remove(existed);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        

        //public async Task<bool> GivePrizeToUser(string id, int userId)
        //{
        //    ContestPrize contestPrize = await _unitOfWork.ContestPrizeRepository.GetFirstOrDefault(
        //        q => q.Id == id);
        //    ContestEventRegister joined = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
        //            q => q.ContestEventId == contestPrize.ContestId && q.UserId == userId &&
        //            q.Status == (int)UserEventContestStatus.Joined);
        //    if (joined != null)
        //    {
        //        contestPrize.UserId = userId;
        //        _unitOfWork.ContestPrizeRepository.Update(contestPrize);
        //        await _unitOfWork.SaveAsync();
        //        return true;
        //    }
        //    return false;
        //}

    }
}
