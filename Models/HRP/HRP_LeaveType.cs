using System.ComponentModel.DataAnnotations;

namespace HRMS.UI.Models.HRP
{
    public class HRP_LeaveType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        [Display(Name = "Leave Name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }

        [Display(Name = "Paid Leave")]
        public bool IsPaid { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
