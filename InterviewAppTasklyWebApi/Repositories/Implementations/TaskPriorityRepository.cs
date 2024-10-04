using InterviewAppTasklyWebApi.Data;
using InterviewAppTasklyWebApi.Entities;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewAppTasklyWebApi.Repositories.Implementations;

public class TaskPriorityRepository : ITaskPriorityRepository
{
    private readonly AppDbContext _context;

    public TaskPriorityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskPriority>> GetAllAsync()
    {
        return await _context.TaskPriorities.ToListAsync();
    }
}