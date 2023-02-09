using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveType.Requests.Queries
{
    public class GetLeaveTypeDetailRequest:IRequest<LeaveTypeDto>
    {
        public int Id { get; set; }
    }
}
