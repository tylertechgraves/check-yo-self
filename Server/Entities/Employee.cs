using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace check_yo_self_api.Server.Entities
{
  [Table("Employee")]
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }
    
    [Required]
    [StringLength(1024)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(1024)]
    public string FirstName { get; set; }
    
    [Required]
    public decimal Salary { get; set; }
    
    [Required]
    public DateTime FirstPaycheckDate { get; set; }
  }
}