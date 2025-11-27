using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Entities;

public class TaskItemTypeConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");          
        builder.HasKey(t => t.Id);         
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(500);         
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("NOW()"); 
    }
}