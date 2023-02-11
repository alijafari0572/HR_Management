using AutoMapper;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistance.Contracts;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands
{

    public class CreateLeaveRequestCommandHandler :
    IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private ILeaveRequestRepository _leaveRequestRepository;
        private IMapper _mapper;
        public CreateLeaveRequestCommandHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaverequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaverequest = await _leaveRequestRepository.Add(leaverequest);
            return leaverequest.Id;
        }
    }


}
