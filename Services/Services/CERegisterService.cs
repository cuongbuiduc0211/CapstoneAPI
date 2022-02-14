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
    public class CERegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MailService _mailService;
        public CERegisterService(IUnitOfWork unitOfWork, MailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }
        public async Task<CERegisStatus> RegisterCE(CERegister register)
        {
            CERegisStatus status = new CERegisStatus();
            DateTime now = DateTime.Now;
            ContestEvent ce = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                    q => q.Id == register.ContestEventId && q.Status == (int)ContestEventStatus.OnGoing &&
                    q.StartRegister <= now && now <= q.EndRegister);            
            List<ContestEventRegister> userRegisters = (await _unitOfWork.CERegisterRepository.GetAll(
                q => q.UserId == register.UserId, null, "ContestEvent")).ToList();
            if (ce == null)
            {
                status.Result = "Đã quá hạn đăng kí cho sự kiện/cuộc thi này";
                return status;
            }
            if (ce.CurrentParticipants >= ce.MaxParticipants)
            {
                status.Result = "Đã quá số lượng người đăng kí cho sự kiện/cuộc thi này";
                return status;
            }
            foreach (var item in userRegisters)
            {
                var itemStartDate = item.ContestEvent.StartDate;
                var itemEndDate = item.ContestEvent.EndDate;
                if (!((itemStartDate > ce.StartDate && itemStartDate > ce.EndDate) ||
                    (itemEndDate < ce.StartDate && itemEndDate < ce.EndDate)))
                {
                    status.Result = "Thời gian diễn ra sự kiện hoặc cuộc thi này đã bị trùng" +
                        " với sự kiện hoặc cuộc thi khác mà bạn đã đăng kí";
                    return status;
                }
            }
            
            ContestEventRegister newRegister = new ContestEventRegister();
            newRegister.Id = Guid.NewGuid().ToString();
            newRegister.ContestEventId = register.ContestEventId;
            newRegister.UserId = register.UserId;
            newRegister.RegisterDate = now;
            newRegister.Status = (int)UserEventContestStatus.Registered;
            ce.CurrentParticipants += 1;
            _unitOfWork.ContestEventRepository.Update(ce);
            await _unitOfWork.CERegisterRepository.Add(newRegister);
            await _unitOfWork.SaveAsync();
            
            User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == register.UserId);
            string userMail = user.Email;
            MailRequest request = new MailRequest();
            request.ToEmail = userMail;
            string start = ce.StartDate.ToString("dd/MM/yyyy hh:mm tt");
            string end = ce.EndDate.ToString("dd/MM/yyyy hh:mm tt");
            if (ce.Type == (int)ContestEventType.Event)
            {
                request.Subject = "Đăng kí sự kiện " + ce.Title + " thành công!";
                request.Body = "Bạn đã đăng kí sự kiện " + ce.Title + " thành công! <br/>"
                    + "Bạn hãy chú ý thời gian diễn ra sự kiện: "
                    + "Từ " + start + " đến " + end + "<br/>"
                    + "Địa điểm diễn ra sự kiện: " + ce.Venue
                    + "<br/><br/>" + "Chúc bạn có một ngày vui vẻ!"
                    + "<br/><br/>" + "Trân trọng, <br/> Car World System";
            }
            else if (ce.Type == (int)ContestEventType.Contest)
            {
                request.Subject = "Đăng kí cuộc thi " + ce.Title + " thành công!";
                request.Body = "Bạn đã đăng kí cuộc thi " + ce.Title + " thành công! <br/>"
                    + "Bạn hãy chú ý thời gian diễn ra cuộc thi: "
                    + "Từ " + start + " đến " + end + "<br/>"
                    + "Địa điểm diễn ra cuộc thi: " + ce.Venue
                    + "<br/><br/>" + "Chúc bạn có một ngày vui vẻ!"
                    + "<br/><br/>" + "Trân trọng, <br/> Car World System";
            }
            try
            {
                await _mailService.SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                status.Result = "Gửi mail không thành công";
            }
            
            status.Result = "Đăng kí thành công";
            return status;
        }

        public async Task<bool> CancelRegisterCE(CERegister register)
        {
            ContestEvent ce = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                                             q => q.Id == register.ContestEventId);
            ContestEventRegister registered = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
                    q => q.ContestEventId == register.ContestEventId && q.UserId == register.UserId
                    && q.Status == (int)UserEventContestStatus.Registered);
            DateTime now = DateTime.Now;
            if (registered != null && ce.StartRegister <= now && now <= ce.EndRegister)
            {
                registered.Status = (int)UserEventContestStatus.Canceled;
                ce.CurrentParticipants -= 1;
                _unitOfWork.ContestEventRepository.Update(ce);
                _unitOfWork.CERegisterRepository.Update(registered);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EvaluateCE(CERegister register, double evaluation)
        {
            ContestEventRegister joined = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
                    q => q.ContestEventId == register.ContestEventId && q.UserId == register.UserId
                    && q.Status == (int)UserEventContestStatus.Joined);
            if (joined != null)
            {
                joined.Evaluation = evaluation;
                _unitOfWork.CERegisterRepository.Update(joined);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ContestEventRegister>> GetCEsRegistered(ContestEventType type, int userId)
        {
            return await _unitOfWork.CERegisterRepository.GetAll(
                q => q.UserId == userId && q.ContestEvent.Type == (int)type &&
                (q.Status == (int)UserEventContestStatus.Registered || 
                q.Status == (int)UserEventContestStatus.Canceled),
                o => o.OrderByDescending(s => s.RegisterDate),
                "ContestEvent");
        }

        public async Task<IEnumerable<ContestEventRegister>> GetCEsJoined(ContestEventType type, int userId)
        {
            return await _unitOfWork.CERegisterRepository.GetAll(
                q => q.UserId == userId && q.ContestEvent.Type == (int)type &&
                q.Status == (int)UserEventContestStatus.Joined,
                o => o.OrderByDescending(s => s.RegisterDate),
                "ContestEvent");
        }

        public async Task<IEnumerable<ContestEventRegister>> GetUsersInCE(string contestEventId)
        {
            return await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == contestEventId && q.Status != (int)UserEventContestStatus.Canceled,
                o => o.OrderBy(s => s.RegisterDate),
                "User");
        }

        public async Task<bool> CheckInUser(CERegister register, UserEventContestStatus status)
        {
            ContestEventRegister user = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
                    q => q.ContestEventId == register.ContestEventId && q.UserId == register.UserId);
            ContestEvent ce = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                q => q.Id == register.ContestEventId);
            DateTime now = DateTime.Now;
            if (user != null && ce.StartDate <= now && now <= ce.EndDate)
            {
                user.Status = (int)status;
                _unitOfWork.CERegisterRepository.Update(user);
                ce.Status = (int)ContestEventStatus.CheckedIn;
                _unitOfWork.ContestEventRepository.Update(ce);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }  
        }

        public async Task<bool> CheckInUsers(CheckInItem items)
        {           
            ContestEvent ce = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                q => q.Id == items.ContestEventId);
            DateTime now = DateTime.Now;
            if (ce != null && ce.StartDate <= now && now <= ce.EndDate)
            {
                List<ContestEventRegister> list = new List<ContestEventRegister>();
                foreach (var item in items.CheckIns)
                {
                    ContestEventRegister user = await _unitOfWork.CERegisterRepository.GetFirstOrDefault(
                       q => q.ContestEventId == items.ContestEventId && q.UserId == item.UserId);
                    user.Status = (int)item.Status;
                    list.Add(user);                                    
                }
                _unitOfWork.CERegisterRepository.UpdateRange(list);
                ce.Status = (int)ContestEventStatus.CheckedIn;
                _unitOfWork.ContestEventRepository.Update(ce);
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
