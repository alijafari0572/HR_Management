using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Application.Features.LeaveType.Requests.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var leaveRequets = await _mediator.Send(new GetLeaveRequestsListRequest());
            return Ok(leaveRequets);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestsDto leaveRequest)
        {
            var cammand = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var respons = await _mediator.Send(cammand);
            return Ok(respons);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var cammand = new UpdateLeaveRequestCommand {Id=id, UpdateLeaveRequestDto = leaveRequest };
            await _mediator.Send(cammand);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/changeapproval/5
        [HttpPut("changeapproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApprovealDto changeLeaveRequestApprovealDto)
        {
            var cammand = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovealDto = changeLeaveRequestApprovealDto };
            await _mediator.Send(cammand);
            return NoContent();
        }
    }
}
