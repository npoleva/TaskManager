using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskItemTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}