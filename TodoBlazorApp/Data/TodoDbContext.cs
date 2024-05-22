using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using TodoBlazorApp.Data.Models;
using System.Collections.Generic;

namespace TodoBlazorApp.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext()
    {   
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<TodoList> TodoLists { get; set; }
     /// <summary>
     /// //////////////
     /// </summary>
     /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("INSERT CONNECTION STRING");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.ToTable("Todolist");

            entity.Property(e => e.Item).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.TodoLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Todolist_User");
        });
    }
}
