namespace TodoBlazorApp.Data.Models;

public partial class TodoList
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Item { get; set; } = null!;

    public virtual User User { get; set; } = null!;

}