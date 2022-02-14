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
    public class FeedbackService
    {
        private readonly NotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        public FeedbackService(NotificationService notificationService, IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
        }
        private string tokenWeb = "eNI8WOneO4a2nmYJ4tEYvA:APA91bGDhvV383puM-d4OteNLjCsh8KKv5viraz2ZpDuZg2ElizHj6Y5rumELQzXww3M5yj760IRTSRFJtp8AHIG1TVtBknmfSelHB7RRLLwNbwl0UKnn-C9186jYiq_xYartCa6ABW7";
        public async Task<bool> FeedbackCE(string contestEventId, FeedbackItem feedbackItem)
        {
            ContestEventRegister joined = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
                q => q.ContestEventId == contestEventId && q.UserId == feedbackItem.FeedbackUserId &&
                q.Status == (int)UserEventContestStatus.Joined);
            
            if (joined != null)
            {
                             
                Feedback feedback = new Feedback()
                {
                    Id = Guid.NewGuid().ToString(),
                    FeedbackUserId = feedbackItem.FeedbackUserId,              
                    Type = (int)FeedbackType.ContestEvent,
                    FeedbackContent = feedbackItem.FeedbackContent,
                    FeedbackDate = DateTime.Now,
                    ContestEventId = contestEventId
                };
                await _unitOfWork.FeedbackRepository.Add(feedback);
                await _unitOfWork.SaveAsync();
                User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == feedbackItem.FeedbackUserId);
                User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Username == "nguyenminhthu");
                ContestEvent ce = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(q => q.Id == contestEventId);
                bool noti = false;
                
                if (ce.Type == (int)ContestEventType.Contest)
                {
                    noti = await _notificationService.SendNoti(tokenWeb, user.Image + "|" + user.FullName + " đã gửi một phản hồi về Cuộc thi",
                    ce.Title + "|" + feedback.FeedbackDate + "|phan-hoi");
                }else if(ce.Type == (int)ContestEventType.Event)
                {
                    noti = await _notificationService.SendNoti(tokenWeb, user.Image + "|" + user.FullName + " đã gửi một phản hồi về Sự kiện",
                    ce.Title + "|" + feedback.FeedbackDate + "|phan-hoi");
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FeedbackExchange(string exchangeId, FeedbackItem feedbackItem)
        {
            Exchange exchange = await _unitOfWork.ExchangeRepository.GetFirstOrDefault(
                q => q.Id == exchangeId && q.UserId == feedbackItem.FeedbackUserId &&
                q.Status == (int)ExchangeStatus.Finished);
            
            if (exchange != null)
            {
                
                Feedback feedback = new Feedback()
                {
                    Id = Guid.NewGuid().ToString(),
                    FeedbackUserId = feedbackItem.FeedbackUserId,
                    Type = (int)FeedbackType.Exchange,
                    FeedbackContent = feedbackItem.FeedbackContent,
                    FeedbackDate = DateTime.Now,
                    ExchangeId = exchangeId
                };
                await _unitOfWork.FeedbackRepository.Add(feedback);
                await _unitOfWork.SaveAsync();
                User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == feedbackItem.FeedbackUserId);
                User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Username == "nguyenminhthu");
                bool noti = false;
                noti = await _notificationService.SendNoti(tokenWeb, user.Image + "|" + user.FullName + " đã gửi một phản hồi về Dịch vụ trao đổi",
                    exchange.Title + "|" + feedback.FeedbackDate + "|phan-hoi");
                return true;
            }
            else
            {
                return false;
            }
        }
      
        public async Task<bool> FeedbackExResponse(string exResId, FeedbackItem feedbackItem)
        {
            ExchangeResponse exResponse = await _unitOfWork.ExchangeResponseRepository.GetFirstOrDefault(
                q => q.Id == exResId && q.UserId == feedbackItem.FeedbackUserId &&
                q.Status == (int)ExchangeStatus.Accepted, "Exchange");
            if (exResponse != null)
            {
                
                Feedback feedback = new Feedback()
                {
                    Id = Guid.NewGuid().ToString(),
                    FeedbackUserId = feedbackItem.FeedbackUserId,
                    Type = (int)FeedbackType.ExchangeResponse,
                    FeedbackContent = feedbackItem.FeedbackContent,
                    FeedbackDate = DateTime.Now,
                    ExchangeResponseId = exResId
                };
                await _unitOfWork.FeedbackRepository.Add(feedback);
                await _unitOfWork.SaveAsync();
                User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == feedbackItem.FeedbackUserId);
                User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Username == "nguyenminhthu");
                bool noti = false;
                noti = await _notificationService.SendNoti(tokenWeb, user.Image + "|" + user.FullName + " đã gửi một phản hồi về Dịch vụ trao đổi!",
                    exResponse.Exchange.Title + "|" + feedback.FeedbackDate + "|phan-hoi");
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public async Task<IEnumerable<Feedback>> GetFeedbacksByType(FeedbackType type)
        {
            return await _unitOfWork.FeedbackRepository.GetAll(
                q => q.Type == (int) type,
                o => o.OrderByDescending(s => s.FeedbackDate),
                "Exchange, ExchangeResponse, ContestEvent, FeedbackUser, ReplyUser");
        }

        public async Task<Feedback> GetFeedbackByType(string id)
        {
            return await _unitOfWork.FeedbackRepository.GetFirstOrDefault(
                q => q.Id == id && q.Type == (int)FeedbackType.ExchangeResponse,
                
                "Exchange, ExchangeResponse, ContestEvent, FeedbackUser, ReplyUser");
        }
        public async Task<IEnumerable<Feedback>> GetUserFeedbacks(int userId)
        {
            return await _unitOfWork.FeedbackRepository.GetAll(
                q => q.FeedbackUserId == userId,
                o => o.OrderBy(s => s.FeedbackDate),
                "Exchange, ExchangeResponse, ContestEvent, ReplyUser");
        }
        public async Task<Feedback> GetFeedbackById(string id)
        {
            return await _unitOfWork.FeedbackRepository.GetFirstOrDefault(
                q => q.Id == id,
                "Exchange, ExchangeResponse, ContestEvent, FeedbackUser, ReplyUser");
        }

        public async Task<bool> ReplyFeedback(string id, ReplyFeedbackItem item)
        {
            Feedback feedback = await _unitOfWork.FeedbackRepository.GetFirstOrDefault(
                q => q.Id == id, "FeedbackUser");
            if (feedback != null && feedback.ReplyContent == null)
            {
                feedback.ReplyUserId = item.ReplyUserId;
                feedback.ReplyContent = item.ReplyContent;
                feedback.ReplyDate = DateTime.Now;
                _unitOfWork.FeedbackRepository.Update(feedback);
                await _unitOfWork.SaveAsync();
                User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == item.ReplyUserId);
                User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == feedback.FeedbackUser.Id);
                bool noti = false;
                noti = await _notificationService.SendNotiToMobile(user.TokenMobile,
                   manager.FullName + " đã trả lời phản hồi của bạn!", feedback.ReplyContent,
                   manager.Image, "phan-hoi");
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
