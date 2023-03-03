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
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exceptions;

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
            var validator = new CreateLeaveTypeValidator();
            var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leavetype = _mapper.Map<Domain.LeaveType>(request.CreateLeaveTypeDto);
            leavetype = await _leaveTypeRepository.Add(leavetype);
            return leavetype.Id;
        }
    }
}
