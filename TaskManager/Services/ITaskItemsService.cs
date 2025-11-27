using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskItemsService
{
    Task<List<TaskItemDto>> GetAllAsync();
    Task<TaskItemDto?> GetByIdAsync(int id);
    Task<TaskItemDto> CreateAsync(TaskItemDto dto);
    Task<TaskItemDto> UpdateAsync(TaskItemDto dto);
    Task<bool> DeleteAsync(int id);
}