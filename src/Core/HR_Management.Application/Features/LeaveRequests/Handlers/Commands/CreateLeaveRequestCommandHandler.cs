using AutoMapper;
using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Models;
using HR_Management.Application.Persistance.Contracts;
using HR_Management.Application.Responses;
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
    IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private ILeaveRequestRepository _leaveRequestRepository;
        private IMapper _mapper;
        private ILeaveTypeRepository _leaveTypeRepository;
        private  IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if (validationResult.IsValid == false)
            {
                //throw new ValidationException(validationResult);
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }

            var leaverequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaverequest = await _leaveRequestRepository.Add(leaverequest);
            response.Success = true;
            response.Message = "Creation Succesfull";
            response.Id=leaverequest.Id;
            //return leaverequest.Id;

            var email = new Email
            {
                To = "iman@madaeny.com",
                Subject = "Leave Request Submitted",
                Body = $"Your leave request for {request.LeaveRequestDto.StartDate} " +
                $"to {request.LeaveRequestDto.EndDate} " +
                $"has been submitted"
            };
            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //log
            }
            return response;
        }
    }


}
