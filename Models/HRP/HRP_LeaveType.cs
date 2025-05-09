namespace HRMS.UI.Models.HRP
{
    public class HRP_LeaveType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
    }
}
