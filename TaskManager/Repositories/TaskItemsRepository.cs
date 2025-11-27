using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

namespace TaskManager.Repositories;

public class TaskItemsRepository : ITaskItemsRepository
{
    private readonly TaskManagerDbContext _context;
    
    public TaskItemsRepository(TaskManagerDbContext context)
    {
        _context = context;
    }
    
    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem> AddAsync(TaskItem entity)
    {
        await _context.Tasks.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TaskItem taskItem)
    {
        _context.Tasks.Update(taskItem);
        await _context.SaveChangesAsync();
        return true; 
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}