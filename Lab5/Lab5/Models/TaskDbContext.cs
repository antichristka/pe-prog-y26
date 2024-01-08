using Microsoft.EntityFrameworkCore;

namespace Lab5.Models;


public class TaskDbContext : DbContext
{
    public DbSet<TaskDto> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Tasks.db");
        base.OnConfiguring(optionsBuilder);
    }
}
