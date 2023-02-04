using HR_Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto
    {
        public LeaveType LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Aoorived { get; set; }
    }
}
