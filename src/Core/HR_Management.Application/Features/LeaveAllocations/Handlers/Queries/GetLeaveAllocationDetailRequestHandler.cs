using AutoMapper;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Application.Features.LeaveAllocations.Requests;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_Management.Application.DTOs.LeaveAllocation;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest,
        LeaveAllocationDto>
    {
        private readonly ILeaveAllocationRepository _LeaveAllocationRepository;
        private readonly IMapper _mapper;
        public GetLeaveAllocationDetailRequestHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _LeaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request,
            CancellationToken cancellationToken)
        {
            var leaveAllocation = await _LeaveAllocationRepository.GetById(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);
        }
    }
}
