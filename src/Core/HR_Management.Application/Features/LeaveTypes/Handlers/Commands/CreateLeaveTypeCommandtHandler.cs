using AutoMapper;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Contracts.Persistance;
using HR_Management;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Responses;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandtHandler : 
        IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private ILeaveTypeRepository _leaveTypeRepository;
        private IMapper _mapper;
        public CreateLeaveTypeCommandtHandler(ILeaveTypeRepository leaveTypeRepository, 
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
			var validator = new CreateLeaveTypeValidator();
            var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Vration Faild";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
				var leavetype = _mapper.Map<Domain.LeaveType>(request.CreateLeaveTypeDto);
				leavetype = await _leaveTypeRepository.Add(leavetype);
				response.Success = true;
				response.Message = "Vration Successful";
                response.Id= leavetype.Id;
			}
            return response;
        }
    }
}
