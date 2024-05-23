using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoBlazorApp.Data.Models;

[Table("users")]
public partial class User
{
    public int Id { get; set; }
    [StringLength(500)]
    public string Name { get; set; }
    [StringLength(500)]
    public string SocialSecurityNumber { get; set; }

    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
}
