using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ApprovalRequest
{
    [Key]
    public int ID { get; set; }
    [Required]
    [ForeignKey("Approver")]
    public int ApproverId { get; set; }
    public Employee Approver { get; set; }
    [Required]
    [ForeignKey("LeaveRequest")]
    public int LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; }
    [Required]
    public string Status { get; set; } = "New";  // default value
    public string Comment { get; set; }
}
