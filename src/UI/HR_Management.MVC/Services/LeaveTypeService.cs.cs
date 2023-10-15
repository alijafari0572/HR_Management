using AutoMapper;
using HR_Management.MVC.Contracts;
using HR_Management.MVC.Models;
using HR_Management.MVC.Services.Base;

namespace HR_Management.MVC.Services
{
	public class LeaveTypeService : BaseHttpService, ILeaveTypeService
	{
		private readonly IMapper mapper;
		private readonly IClient httpClient;
		private readonly ILocalStorageService localStorageService;
		public LeaveTypeService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService)
			: base(httpClient, localStorageService)
		{
			this.mapper = mapper;
			this.httpClient = httpClient;
			this.localStorageService = localStorageService;
		}
		public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
		{
			try
			{
				var response = new Response<int>();
				CreateLeaveTypeDto createLeaveTypeDto =
					mapper.Map<CreateLeaveTypeDto>(leaveType);
				//TODO Auth
				var apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);

				if (apiResponse.Success)
				{
					response.Data = apiResponse.Id;
					response.Success = true;
				}
				else
				{
					foreach (var err in apiResponse.Errors)
					{
						response.ValidationErrors += err + Environment.NewLine;
					}
				}
				return response;
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}
		public async Task<Response<int>> DeleteLeaveType(int id)
		{
			try
			{
				await _client.LeaveTypesDELETEAsync(id);
				return new Response<int> { Success = true };
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}
		public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
		{
			var leaveType = await _client.LeaveTypesGETAsync(id);
			return mapper.Map<LeaveTypeVM>(leaveType);
		}
		public async Task<List<LeaveTypeVM>> GetLeaveTypes()
		{
			var leaveTypes = await _client.LeaveTypesAllAsync();
			return mapper.Map<List<LeaveTypeVM>>(leaveTypes);
		}
		public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
		{
			try
			{
				LeaveTypeDto leaveTypeDto = mapper.Map<LeaveTypeDto>(leaveType);
				await _client.LeaveTypesPUTAsync(id, leaveTypeDto);
				return new Response<int> { Success = true };
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}
	}
}
