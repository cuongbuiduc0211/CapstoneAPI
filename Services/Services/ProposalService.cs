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
    public class ProposalService
    {
        private readonly NotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProposalService(NotificationService notificationService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNewProposal(ProposalItem proposalItem)
        {
            User checkUser = await _unitOfWork.UserRepository.GetFirstOrDefault(
                                                           q => q.Id == proposalItem.UserID);
            if (checkUser.RoleId == (int)UserRole.User)
            {
                if (proposalItem.StartDate <= proposalItem.EndDate)
                {
                    Proposal newProposal = _mapper.Map<Proposal>(proposalItem);
                    newProposal.Id = Guid.NewGuid().ToString();
                    //newProposal.Type = (int)proposalItem.Type;
                    newProposal.Status = (int)ProposalStatus.UnApproved;
                    newProposal.CreatedDate = DateTime.Now;
                    await _unitOfWork.ProposalRepository.Add(newProposal);
                    await _unitOfWork.SaveAsync();
                    User user = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Id == proposalItem.UserID);
                    bool noti = false;
                    User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(q => q.Username == "nguyenminhthu");
                    var tokenWeb = "eNI8WOneO4a2nmYJ4tEYvA:APA91bGDhvV383puM-d4OteNLjCsh8KKv5viraz2ZpDuZg2ElizHj6Y5rumELQzXww3M5yj760IRTSRFJtp8AHIG1TVtBknmfSelHB7RRLLwNbwl0UKnn-C9186jYiq_xYartCa6ABW7";
                    noti = await _notificationService.SendNoti(tokenWeb, user.Image + "|" + user.FullName + " đã gửi một đề xuất!",
                        newProposal.Title + "|" + newProposal.CreatedDate + "|de-xuat");
                    return true;
                }
            }                                                     
            return false;          
        }

        public async Task<int> CountProposalsByMonth(DateTime date)
        {
            var count = (await _unitOfWork.ProposalRepository.GetAll(
                q => q.CreatedDate.Month == date.Month && q.CreatedDate.Year == date.Year)).Count();
            return count;
        }

        public async Task<IEnumerable<Proposal>> GetAllProposal()
        {
            return await _unitOfWork.ProposalRepository.GetAll(
                null,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, Manager");
        }
        public async Task<IEnumerable<Proposal>> GetAllProposalEvent()
        {
            return await _unitOfWork.ProposalRepository.GetAll(
                q => q.Type == (int)ProposalType.Event,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, Manager");
        }
        public async Task<IEnumerable<Proposal>> GetAllProposalContest()
        {
            return await _unitOfWork.ProposalRepository.GetAll(
                q => q.Type == (int)ProposalType.Contest,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, Manager");
        }
        public async Task<Proposal> GetProposalById(string id)
        {
            return await _unitOfWork.ProposalRepository.GetFirstOrDefault(
                q => q.Id == id,
                "User, Manager");
        }
        public async Task<IEnumerable<Proposal>> GetProposalsUserSubmited(int userId)
        {
            return await _unitOfWork.ProposalRepository.GetAll(
                q => q.UserId == userId,
                o => o.OrderByDescending(s => s.CreatedDate),
                "User, Manager");
        }
        public async Task<bool> ApprovedProposal(StatusProposalItem item)
        {
            Proposal proposal = await _unitOfWork.ProposalRepository.GetFirstOrDefault(
                q => q.Id == item.id && q.Status == (int)ProposalStatus.UnApproved, "User");
            User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(
                                                          q => q.Id == item.ManagerId);
            if (proposal != null && manager.RoleId == (int)UserRole.Manager)
            {
                proposal.Status = (int)ProposalStatus.Approved;
                proposal.ManagerId = item.ManagerId;
                _unitOfWork.ProposalRepository.Update(proposal);
                await _unitOfWork.SaveAsync();
                bool noti = false;
                noti = await _notificationService.SendNotiToMobile(proposal.User.TokenMobile,
                manager.FullName + " đã chấp nhận đề xuất của bạn!", proposal.Title,
                manager.Image, "de-xuat");
                return true;
            }
            return false;           
        }
        public async Task<bool> DisApprovedProposal(StatusProposalItem item)
        {
            Proposal proposal = await _unitOfWork.ProposalRepository.GetFirstOrDefault(
                q => q.Id == item.id && q.Status == (int)ProposalStatus.UnApproved, "User");
            User manager = await _unitOfWork.UserRepository.GetFirstOrDefault(
                                                          q => q.Id == item.ManagerId);
            if (proposal != null && manager.RoleId == (int)UserRole.Manager)
            {
                proposal.Status = (int)ProposalStatus.DisApproved;
                proposal.Reason = item.Reason;
                proposal.ManagerId = item.ManagerId;
                _unitOfWork.ProposalRepository.Update(proposal);
                await _unitOfWork.SaveAsync();
                bool noti = false;
                noti = await _notificationService.SendNotiToMobile(proposal.User.TokenMobile,
                manager.FullName + " đã từ chối đề xuất của bạn!", proposal.Title,
                manager.Image, "de-xuat");
                return true;
            }
            return false;
        }
    }
}
