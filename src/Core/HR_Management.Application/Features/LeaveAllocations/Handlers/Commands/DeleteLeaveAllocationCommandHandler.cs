﻿using AutoMapper;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Contracts.Persistance;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetById(request.Id);
            if (leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            await _leaveAllocationRepository.Delete(leaveAllocation);
            return Unit.Value;
        }
    }
}
