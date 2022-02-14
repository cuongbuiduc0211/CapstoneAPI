using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace Services.Services
{
    public class ExchangeResponseService
    {
        private readonly NotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExchangeResponseService(NotificationService notificationService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> SendExResponse(ExchangeResItem exchangeResItem)
        {
            Exchange exchange = await _unitOfWork.ExchangeRepository.GetFirstOrDefault(
                q => q.Id == exchangeResItem.ExchangeId && q.Status == (int)ExchangeStatus.InProcess);
            ExchangeResponse existed = await _unitOfWork.ExchangeResponseRepository.GetFirstOrDefault(
                q => q.ExchangeId == exchangeResItem.ExchangeId && q.UserId == exchangeResItem.UserId);
            if (exchange != null && existed == null)
            {
                ExchangeResponse response = _mapper.Map<ExchangeResponse>(exchangeResItem);
                response.Id = Guid.NewGuid().ToString();
                response.CreatedDate = DateTime.Now;
                response.Status = (int)ExchangeStatus.InProcess;
                await _unitOfWork.ExchangeResponseRepository.Add(response);
                await _unitOfWork.SaveAsync();
                User owner = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == exchange.UserId);
                User responseUser = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == response.UserId);
                bool noti = false;
                noti = await _notificationService.SendNotiToMobile(owner.TokenMobile,
                    responseUser.FullName + " đã gửi cho bạn 1 yêu cầu trao đổi!", exchange.Title,
                    responseUser.Image, "yeu-cau-trao-doi");
                return true;
            }
            return false;
        }
       
        public async Task<IEnumerable<ExchangeResponse>> GetExResponses(string exchangeId)
        {
            return await _unitOfWork.ExchangeResponseRepository.GetAll(
                q => q.ExchangeId == exchangeId,
                o => o.OrderBy(s => s.CreatedDate),
                "User");
        }
        public async Task<IEnumerable<ExchangeResponse>> GetInProcessResponses(int userId)
        {
            return await _unitOfWork.ExchangeResponseRepository.GetAll(
                q => q.UserId == userId && q.Status == (int)ExchangeStatus.InProcess,
                o => o.OrderBy(s => s.CreatedDate),
                "Exchange");
        }

        public async Task<IEnumerable<ExchangeResponse>> GetAcceptedResponses(int userId)
        {
            return await _unitOfWork.ExchangeResponseRepository.GetAll(
                q => q.UserId == userId && q.Status == (int)ExchangeStatus.Accepted,
                o => o.OrderByDescending(s => s.CreatedDate),
                "Exchange");
        }

        public async Task<string> AcceptExResponse(string exchangeId, int userId)
        {
            IEnumerable<ExchangeResponse> responses = await _unitOfWork.ExchangeResponseRepository.GetAll(
                q => q.ExchangeId == exchangeId);
            foreach (var res in responses)
            {
                if (res.UserId == userId)
                {
                    res.Status = (int)ExchangeStatus.Accepted;
                }
                else
                {
                    res.Status = (int)ExchangeStatus.Denied;
                }
            }
            Exchange exchange = await _unitOfWork.ExchangeRepository.GetFirstOrDefault(
                q => q.Id == exchangeId, "User");
            exchange.Status = (int)ExchangeStatus.Finished;
            exchange.User.ExchangePost -= 1;
            _unitOfWork.ExchangeResponseRepository.UpdateRange(responses);
            _unitOfWork.ExchangeRepository.Update(exchange);
            await _unitOfWork.SaveAsync();
            User owner = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == exchange.UserId);
            User responseUser = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == userId);
            bool noti = false;
            noti = await _notificationService.SendNotiToMobile(responseUser.TokenMobile,
                    owner.FullName + " đã chấp nhận yêu cầu trao đổi của bạn!", exchange.Title,
                    owner.Image, "chap-nhan-trao-doi");
            return "You have accepted this response";

        }      
    }
}
