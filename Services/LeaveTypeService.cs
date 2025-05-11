using HRMS.UI.Data;
using HRMS.UI.Models.HRP;
using HRMS.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.UI.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LeaveTypeService> _logger;

        public LeaveTypeService(ApplicationDbContext context, ILogger<LeaveTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<HRP_LeaveType>> GetAllAsync()
            => await _context.HRP_LeaveType.OrderBy(x => x.Name).ToListAsync();

        public async Task<HRP_LeaveType?> GetByIdAsync(int id)
            => await _context.HRP_LeaveType.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(HRP_LeaveType leaveType)
        {
            try
            {
                _context.HRP_LeaveType.Add(leaveType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding LeaveType");
                throw;
            }
        }

        public async Task UpdateAsync(HRP_LeaveType leaveType)
        {
            try
            {
                _context.HRP_LeaveType.Update(leaveType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating LeaveType");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.HRP_LeaveType.FindAsync(id);
            if (entity is null) return;

            try
            {
                _context.HRP_LeaveType.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting LeaveType with Id {id}");
                throw;
            }
        }

        public async Task<List<HRP_LeaveType>> FilterAsync(string? name, string? isPaid, string? isActive)
        {
            var query = _context.HRP_LeaveType.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            if (bool.TryParse(isPaid, out var paid))
                query = query.Where(x => x.IsPaid == paid);

            if (bool.TryParse(isActive, out var active))
                query = query.Where(x => x.IsActive == active);

            return await query.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
