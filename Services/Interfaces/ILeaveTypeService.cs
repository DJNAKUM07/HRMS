using HRMS.UI.Models.HRP;

namespace HRMS.UI.Services.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<List<HRP_LeaveType>> GetAllAsync();
        Task<HRP_LeaveType> GetByIdAsync(int id);
        Task AddAsync(HRP_LeaveType leaveType);
        Task UpdateAsync(HRP_LeaveType leaveType);
        Task DeleteAsync(int id);
    }
}
