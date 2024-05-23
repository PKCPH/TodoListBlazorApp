using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TodoBlazorApp.Data.Models;

[Table("todos")]
public partial class Todo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [StringLength(500)]
    public string Item { get; set; } = null!;

    public virtual User User { get; set; } = null!;

}