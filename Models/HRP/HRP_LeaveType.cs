using System.ComponentModel.DataAnnotations;

namespace HRMS.UI.Models.HRP
{
    public class HRP_LeaveType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
    }
}
