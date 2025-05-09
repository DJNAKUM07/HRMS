using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRMS.UI.Models.HRP; // Import the namespace for HRP_LeaveType

namespace HRMS.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HRP_LeaveType> HRP_LeaveType { get; set; }
    }
}
