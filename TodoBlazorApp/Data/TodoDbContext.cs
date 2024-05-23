using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using TodoBlazorApp.Data.Models;
using System.Collections.Generic;

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
