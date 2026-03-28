using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCtest.Models;

public class ApplicationUser:IdentityUser
{
    [Required]
    public int Name { get; set; }
    public string Address { get; set; }
}