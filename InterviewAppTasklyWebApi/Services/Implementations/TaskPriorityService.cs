using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using InterviewAppTasklyWebApi.Services.Interfaces;

namespace InterviewAppTasklyWebApi.Services.Implementations;

public class TaskPriorityService : ITaskPriorityService
{
    private readonly ITaskPriorityRepository _taskPriority;

    public TaskPriorityService(ITaskPriorityRepository taskPriority)
    {
        _taskPriority = taskPriority;
    }

    public async Task<IEnumerable<TaskPriorityModel>> GetAllAsync()
    {
        var priorities = await _taskPriority.GetAllAsync();

        var prioritiesList = priorities.Select(priority => new TaskPriorityModel
        {
            Id = priority.Id,
            Name = priority.Name,
        });
        
        return prioritiesList;
    }
}