using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Extensions;

public static class TaskItemExtensions
{
    public static TaskItem ToEntity(this TaskItemDto dto)
    {
        return new TaskItem
        {
            Id = dto.Id, 
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted
        };
    }

    public static TaskItemDto ToDto(this TaskItem entity)
    {
        return new TaskItemDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            IsCompleted = entity.IsCompleted,
            CreatedAt = entity.CreatedAt
        };
    }
}