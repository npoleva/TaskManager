using Microsoft.EntityFrameworkCore;
using TaskManager.Extensions;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services;

public class TaskItemsService  : ITaskItemsService
{
    private readonly ITaskItemsRepository _repository;
    private readonly ILogger<TaskItemsService> _logger;

    public TaskItemsService(ITaskItemsRepository repository, ILogger<TaskItemsService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<List<TaskItemDto>> GetAllAsync()
    {
        try
        {
            var tasks = await _repository.GetAllAsync();
            return tasks.Select(t => t.ToDto()).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении всех задач");
            throw;
        }
    }
    
    public async Task<TaskItemDto?> GetByIdAsync(int id)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Задача с id {id} не найдена");

            return task.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении задачи с Id {Id}", id);
            throw;
        }
    }
    
    public async Task<TaskItemDto> CreateAsync(TaskItemDto dto)
    {
        try
        {
            dto.Id = 0;
            
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new InvalidOperationException("Заголовок задачи не может быть пустым");
            
            var entity = dto.ToEntity();
            var created = await _repository.AddAsync(entity);
            return created.ToDto();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка при создании задачи");
            throw new InvalidOperationException("Не удалось создать задачу в базе данных", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании задачи");
            throw;
        }
    }
    
    public async Task<TaskItemDto> UpdateAsync(TaskItemDto dto)
    {
        try
        {
            if (dto.Id == 0)
                throw new InvalidOperationException("Невозможно обновить задачу без Id");
            
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new InvalidOperationException("Заголовок задачи не может быть пустым");

            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Задача с id {dto.Id} не найдена");

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.IsCompleted = dto.IsCompleted;

            await _repository.UpdateAsync(existing);
            return existing.ToDto();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении задачи с Id {Id}", dto.Id);
            throw new InvalidOperationException("Не удалось обновить задачу в базе данных", ex);
        }
        catch (Exception ex) when (!(ex is KeyNotFoundException))
        {
            _logger.LogError(ex, "Ошибка при обновлении задачи с Id {Id}", dto.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                throw new KeyNotFoundException($"Task with id {id} not found");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка при удалении задачи с Id {Id}", id);
            throw new InvalidOperationException("Не удалось удалить задачу из базы данных", ex);
        }
        catch (Exception ex) when (!(ex is KeyNotFoundException))
        {
            _logger.LogError(ex, "Ошибка при удалении задачи с Id {Id}", id);
            throw;
        }

        return true;
    }
}