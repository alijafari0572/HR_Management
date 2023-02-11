using AutoMapper;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Persistance.Contracts;
using HR_Management;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandtHandler : 
        IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private ILeaveTypeRepository _leaveTypeRepository;
        private IMapper _mapper;
        public CreateLeaveTypeCommandtHandler(ILeaveTypeRepository leaveTypeRepository, 
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leavetype = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);
            leavetype = await _leaveTypeRepository.Add(leavetype);
            return leavetype.Id;
        }
    }
}
