using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models;

namespace CarWorldAPI.Controllers
{
    [Route("api/proposal")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly ProposalService _proposalService;
        public ProposalController(ProposalService proposalService)
        {
            _proposalService = proposalService;
        }
        //Create new accessory
        [HttpPost("CreateNewProposal")]
        public async Task<IActionResult> CreateNewProposal([FromBody] ProposalItem proposalItem)
        {
            bool check = await _proposalService.CreateNewProposal(proposalItem);
            if (check)
            {
                return Ok("Create new Proposal successfully!");
            }
            else
            {
                return BadRequest("You are not allowed to use this feature!");
            }

        }

        [HttpGet("CountProposalsByMonth")]
        public async Task<IActionResult> CountProposalsByMonth(DateTime date)
        {
            var result = await _proposalService.CountProposalsByMonth(date);
            return Ok(result);
        }

        //Get all Proposal
        [HttpGet("GetAllProposal")]
        public async Task<IActionResult> GetAllProposal()
        {
            var result = await _proposalService.GetAllProposal();
            return Ok(result);
        }
        //Get Proposal by id
        [HttpGet("GetProposalById")]
        public async Task<IActionResult> GetProposalById(string id)
        {
            var result = await _proposalService.GetProposalById(id);
            return Ok(result);
        }
        //Get All Proposal Event
        [HttpGet("GetAllProposalEvent")]
        public async Task<IActionResult> GetAllProposalEvent()
        {
            var result = await _proposalService.GetAllProposalEvent();
            return Ok(result);
        }
        //Get All Proposal Contest
        [HttpGet("GetAllProposalContest")]
        public async Task<IActionResult> GetAllProposalContest()
        {
            var result = await _proposalService.GetAllProposalContest();
            return Ok(result);
        }

        [HttpGet("GetProposalsUserSubmited")]
        public async Task<IActionResult> GetProposalsUserSubmited(int userId)
        {
            var result = await _proposalService.GetProposalsUserSubmited(userId);
            return Ok(result);
        }

        //Approved Proposal
        [HttpPut("ApprovedProposal")]
        public async Task<IActionResult> ApprovedProposal([FromBody] StatusProposalItem popsalStatus)
        {
            bool check = await _proposalService.ApprovedProposal(popsalStatus);
            if (check)
            {
                return Ok("Approved Proposal successfully!");
            }
            else
            {
                return BadRequest("Approved Proposal fail!");
            }
        }
        //DisApproved Proposal
        [HttpPut("DisApprovedProposal")]
        public async Task<IActionResult> DisApprovedProposal([FromBody] StatusProposalItem popsalStatus)
        {
            bool check = await _proposalService.DisApprovedProposal(popsalStatus);
            if (check)
            {
                return Ok("DisApproved Proposal successfully!");
            }
            else
            {
                return BadRequest("DisApproved Proposal fail!");
            }
        }
    }
}
