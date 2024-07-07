using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Project
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string ProjectType { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    [ForeignKey("ProjectManager")]
    public int ProjectManagerId { get; set; }
    public Employee ProjectManager { get; set; }
    public string? Comment { get; set; } //Can be empty
    [Required]
    public string Status { get; set; } = "Active";  // Default value
}
