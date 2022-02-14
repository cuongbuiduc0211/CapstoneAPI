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
    public class ContestEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MailService _mailService;
        public ContestEventService(IUnitOfWork unitOfWork, IMapper mapper, MailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<bool> CreateCE(ContestEventItem item)
        {
            ContestEvent existed = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                                                        q => q.Title.ToLower().Equals(item.Title));
            if (existed != null)
            {
                return false;
            }
            else
            {
                if (item.StartRegister <= item.EndRegister &&
                    item.EndRegister <= item.StartDate &&
                    item.StartDate <= item.EndDate)
                {
                    ContestEvent newCE = _mapper.Map<ContestEvent>(item);
                    newCE.Id = Guid.NewGuid().ToString();
                    newCE.Status = (int)ContestEventStatus.OnGoing;
                    newCE.CreatedDate = DateTime.Now;
                    await _unitOfWork.ContestEventRepository.Add(newCE);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<double> TotalMoneyFromCEsByMonth(ContestEventType type, DateTime date)
        {
            var contestEvents = await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.CreatedDate.Month == date.Month
                && q.CreatedDate.Year == date.Year);
            double total = 0;
            foreach (var ce in contestEvents)
            {
                total += (ce.Fee * (double)ce.CurrentParticipants);
            }
            return total;
        }
        public async Task<int> CountCEsByMonth(ContestEventType type,DateTime date)
        {
            var count = (await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.CreatedDate.Month == date.Month 
                && q.CreatedDate.Year == date.Year)).Count();
            return count;
        }
        public async Task<IEnumerable<ContestEvent>> GetCanceledCEs(ContestEventType type)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int) type && q.Status == (int)ContestEventStatus.Canceled,
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        public async Task<IEnumerable<ContestEvent>> GetAllContestPrizes(DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)ContestEventType.Contest && now.Date <= q.EndDate.Date &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        
        
        public async Task<IEnumerable<ContestEvent>> GetCEsMobile(ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.StartRegister <= now && now <= q.EndRegister &&
                (q.Status == (int)ContestEventStatus.OnGoing),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters");
        }

        public async Task<IEnumerable<ContestEvent>> GetCEsByBrandMobile(ContestEventType type, string brandId, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.StartRegister <= now && now <= q.EndRegister &&
                q.BrandId == brandId && q.Status == (int)ContestEventStatus.OnGoing,
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters"); 
        }
        public async Task<IEnumerable<ContestEvent>> GetCEsByUserInterestedBrands(ContestEventType type, List<string> interestedBrands, DateTime now)
        {
            List<ContestEvent> contestEventsByUIB = new List<ContestEvent>();
            foreach (var interested in interestedBrands)
            {
                List<ContestEvent> contestEvents = (await _unitOfWork.ContestEventRepository.GetAll(
                    q => q.Type == (int)type && q.StartRegister <= now && now <= q.EndRegister &&
                    q.BrandId == interested && q.Status == (int)ContestEventStatus.OnGoing,
                    o => o.OrderByDescending(s => s.CreatedDate),
                    "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters")).ToList();
                contestEventsByUIB.AddRange(contestEvents);
            }
            return contestEventsByUIB;
        }
        public async Task<IEnumerable<ContestEvent>> GetOngoingCEsMobile(ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.StartDate.Date <= now.Date && now.Date <= q.EndDate.Date &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }

        public async Task<IEnumerable<ContestEvent>> GetRegisterCEsWeb(ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && now <= q.EndRegister &&
                (q.Status == (int)ContestEventStatus.OnGoing),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters");
        }
        public async Task<IEnumerable<ContestEvent>> GetRegisterCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q =>  q.BrandId == brandId && q.Type == (int)type && now <= q.EndRegister &&
                (q.Status == (int)ContestEventStatus.OnGoing),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters");
        }
        public async Task<IEnumerable<ContestEvent>> GetPreparedCEsWeb(ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.EndRegister < now && now < q.StartDate &&
                (q.Status == (int)ContestEventStatus.OnGoing),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        public async Task<IEnumerable<ContestEvent>> GetPreparedCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.BrandId == brandId && q.Type == (int)type && q.EndRegister < now && now < q.StartDate &&
                (q.Status == (int)ContestEventStatus.OnGoing),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        public async Task<IEnumerable<ContestEvent>> GetOngoingCEsWeb(ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.StartDate <= now && now <= q.EndDate &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        public async Task<IEnumerable<ContestEvent>> GetOngoingCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            return await _unitOfWork.ContestEventRepository.GetAll(
                q => q.BrandId == brandId && q.Type == (int)type && q.StartDate <= now && now <= q.EndDate &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
        }
        public async Task<IEnumerable<ContestEvent>> GetFinishedCEsWeb(ContestEventType type, DateTime now)
        {
            var list = await _unitOfWork.ContestEventRepository.GetAll(
                q => q.Type == (int)type && q.EndDate < now &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
            foreach (var item in list)
            {
                IEnumerable<ContestEventRegister> userEvaluation = await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == item.Id && q.Status == (int)UserEventContestStatus.Joined);
                item.Rating = userEvaluation.Average(e => e.Evaluation);
            }
            _unitOfWork.ContestEventRepository.UpdateRange(list);
            await _unitOfWork.SaveAsync();
            return list;
        }
        public async Task<IEnumerable<ContestEvent>> GetFinishedCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            var list = await _unitOfWork.ContestEventRepository.GetAll(
                q => q.BrandId == brandId && q.Type == (int)type && q.EndDate < now &&
                (q.Status == (int)ContestEventStatus.OnGoing || q.Status == (int)ContestEventStatus.CheckedIn),
                o => o.OrderByDescending(s => s.CreatedDate),
                "CreatedByNavigation, ModifiedByNavigation, Proposal");
            foreach (var item in list)
            {
                IEnumerable<ContestEventRegister> userEvaluation = await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == item.Id && q.Status == (int)UserEventContestStatus.Joined);
                item.Rating = userEvaluation.Average(e => e.Evaluation);
            }
            _unitOfWork.ContestEventRepository.UpdateRange(list);
            await _unitOfWork.SaveAsync();
            return list;
        }
        public async Task<ContestEvent> GetCEById(string id)
        {
            return await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                q => q.Id == id,
                "CreatedByNavigation, ModifiedByNavigation, Proposal, ContestEventRegisters, Brand");
        }

        public async Task<bool> UpdateCE(string id, ContestEventItem item)
        {
            ContestEvent existed = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(q => q.Id == id);
            if (existed != null)
            {
                if (item.StartRegister <= item.EndRegister &&
                    item.EndRegister <= item.StartDate &&
                    item.StartDate <= item.EndDate)
                {
                    existed = _mapper.Map<ContestEventItem, ContestEvent>(item);
                    existed.ModifiedDate = DateTime.Now;
                    existed.Id = id;
                    _unitOfWork.ContestEventRepository.Update(existed);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CancelCE(string id, string reason)
        {
            ContestEvent existed = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(
                q => q.Id == id && q.Status != (int)ContestEventStatus.Canceled);
            IEnumerable<ContestEventRegister> listRegister = await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == id && q.Status == (int)UserEventContestStatus.Registered,
                null, "User");
            MailRequests request = new MailRequests();
            request.ToEmail = new List<string>();
            foreach (var register in listRegister)
            {
                string userMail = register.User.Email;
                request.ToEmail.Add(userMail);
            }
            if (existed != null)
            {
                existed.Status = (int)ContestEventStatus.Canceled;
                _unitOfWork.ContestEventRepository.Update(existed);
                await _unitOfWork.SaveAsync();
                string start = existed.StartDate.ToString("dd/MM/yyyy hh:mm tt");
                string end = existed.EndDate.ToString("dd/MM/yyyy hh:mm tt");
                if (existed.Type == (int)ContestEventType.Event)
                {
                    request.Subject = "Sự kiện " + existed.Title + " đã bị hủy!";
                    request.Body = "Sự kiện " + existed.Title + " diễn ra từ " + start + " đến " + end 
                        + " đã bị hủy vì một vài sự cố:<br/>" + reason 
                        + "<br/><br/>Thành thật xin lỗi bạn vì sự bất tiện này!<br/><br/>" 
                        + "Trân trọng, <br/> Car World System";
                }
                else if (existed.Type == (int)ContestEventType.Contest)
                {
                    request.Subject = "Cuộc thi " + existed.Title + " đã bị hủy!";
                    request.Body = "Cuộc thi " + existed.Title + " diễn ra từ " + start + " đến " + end
                        + " đã bị hủy vì một vài sự cố:<br/>" + reason 
                        + "<br/><br/>Thành thật xin lỗi bạn vì sự bất tiện này!<br/><br/>"
                        + "Trân trọng, <br/> Car World System";
                }
                try
                {
                    await _mailService.SendMultiEmailAsync(request);
                }catch(Exception ex)
                {

                }
                
                return true;
            }
            return false;
        }
        public async Task<double?> RatingCE(string id)
        {
            ContestEvent existed = await _unitOfWork.ContestEventRepository.GetFirstOrDefault(q => q.Id == id);           
            IEnumerable<ContestEventRegister> userEvaluation = await _unitOfWork.CERegisterRepository.GetAll(
                q => q.ContestEventId == id && q.Status == (int)UserEventContestStatus.Joined);
            existed.Rating = userEvaluation.Average(e => e.Evaluation);
            _unitOfWork.ContestEventRepository.Update(existed);
            await _unitOfWork.SaveAsync();
            return existed.Rating;
        }
    }
}
