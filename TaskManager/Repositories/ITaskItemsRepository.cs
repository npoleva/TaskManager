using TaskManager.Entities;

namespace TaskManager.Repositories;

public interface ITaskItemsRepository
{
    Task<TaskItem?> GetByIdAsync(int id);   
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem> AddAsync(TaskItem entity);
    Task<bool> UpdateAsync(TaskItem entity);
    Task<bool> DeleteAsync(int id);
}