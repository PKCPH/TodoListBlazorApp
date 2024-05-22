namespace TodoBlazorApp.Data.Models;

public partial class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SocialSecurityNumber { get; set; }

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
}
