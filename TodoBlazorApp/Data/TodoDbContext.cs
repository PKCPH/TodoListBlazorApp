using Microsoft.EntityFrameworkCore;
using TodoBlazorApp.Data.Models;

namespace TodoBlazorApp.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
    {
    }

    #region DBSets
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Todo> Todos { get; set; }
    #endregion
}
