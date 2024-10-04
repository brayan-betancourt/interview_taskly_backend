using InterviewAppTasklyWebApi.Data;
using InterviewAppTasklyWebApi.Entities;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewAppTasklyWebApi.Repositories.Implementations;

public class TaskStateRepository : ITaskStateRepository
{
    private readonly AppDbContext _context;

    public TaskStateRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TaskState>> GetAllStatesAsync()
    {
        return await _context.TaskStates.ToListAsync();
    }
}