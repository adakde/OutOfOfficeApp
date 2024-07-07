using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LeaveRequest
{
    [Key]
    public int ID { get; set; }
    [Required]
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    [Required]
    public string AbsenceReason { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public string? Comment { get; set; }
    [Required]
    public string Status { get; set; } = "New";  // default value
}
