using HRMS.UI.Data;
using HRMS.UI.Models.HRP;
using HRMS.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.UI.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HRP_LeaveType>> GetAllAsync()
            => await _context.HRP_LeaveType.ToListAsync();

        public async Task<HRP_LeaveType> GetByIdAsync(int id)
            => await _context.HRP_LeaveType.FindAsync(id);

        public async Task AddAsync(HRP_LeaveType leaveType)
        {
            _context.HRP_LeaveType.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HRP_LeaveType leaveType)
        {
            _context.HRP_LeaveType.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.HRP_LeaveType.FindAsync(id);
            if (entity != null)
            {
                _context.HRP_LeaveType.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
