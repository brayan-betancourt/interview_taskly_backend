using InterviewAppTasklyWebApi.Data;
using InterviewAppTasklyWebApi.Entities;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewAppTasklyWebApi.Repositories.Implementations;

public class TaskManagementRepository : ITaskManagementRepository
{
    private readonly AppDbContext _context;

    public TaskManagementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskManagement>> GetAllTasksAsync()
    {
        return await _context.TaskManagements
            .Include(t => t.Priorities)
            .Include(t => t.States)
            .Include(t => t.Users)
            .ToListAsync();
    }

    public async Task<TaskManagement> GetTaskByIdAsync(int id)
    {
        return await _context.TaskManagements
            .Include(t => t.Priorities)
            .Include(t => t.States)
            .Include(t => t.Users)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task AddTaskAsync(TaskManagement task)
    {
        _context.TaskManagements.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskManagement task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.TaskManagements.FindAsync(id);
        
        if (task != null)
        {
            _context.TaskManagements.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> TaskExistsAsync(int id)
    {
        return await _context.TaskManagements.AnyAsync(e => e.Id == id);
    }
}