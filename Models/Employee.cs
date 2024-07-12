using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Subdivision { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    public string Status { get; set; }

    [ForeignKey("PeoplePartner")]
    public int? PeoplePartnerId { get; set; }  // Allow null for PeoplePartnerId to avoid circular dependencies at creation
    public Employee? PeoplePartner { get; set; }

    [Required]
    public int OutOfOfficeBalance { get; set; }

    public byte[]? Photo { get; set; } // Optional

}
