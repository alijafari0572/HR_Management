using HR_Management.MVC.Models;

namespace HR_Management.MVC.Contracts
{
	public interface ILeaveTypeService
	{
		Task<List<LeaveTypeVM>> GetLeaveTypes();
		Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
	}
}
