using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCtest.Models;

public class Category
{
    
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    [DisplayName("類別名稱")]
    public string Name { get; set; }
    [DisplayName("顯示順序")]
    [Range(1,200 ,ErrorMessage = "輸入應該要在1-200之間")]
    public int DisplayOrder { get; set; }
}