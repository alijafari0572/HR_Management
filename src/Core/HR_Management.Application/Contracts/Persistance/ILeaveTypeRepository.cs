using HR_Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Contracts.Persistance
{
    public interface ILeaveTypeRepository:IGenericRepository<LeaveType>
    {
    }
}
