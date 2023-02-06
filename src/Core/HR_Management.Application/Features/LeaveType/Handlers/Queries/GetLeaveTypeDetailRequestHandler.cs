using AutoMapper;
using HR_Management.Application.DTOs;
using HR_Management.Application.Features.LeaveType.Requests;
using HR_Management.Application.Features.LeaveType.Requests.Queries;
using HR_Management.Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, 
        LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, 
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request,
            CancellationToken cancellationToken)
        {
            var leavetype = await _leaveTypeRepository.GetById(request.Id);
            return _mapper.Map<LeaveTypeDto>(leavetype);
        }
    }
}
