using System.ComponentModel.DataAnnotations;
namespace MVCtest.Models;

public class denny
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int age { get; set; }
    public int DisplayOrder { get; set; }
}
