using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using InterviewAppTasklyWebApi.Services.Interfaces;

namespace InterviewAppTasklyWebApi.Services.Implementations;

public class TaskStateService : ITaskStateService
{
    private readonly ITaskStateRepository _taskState;

    public TaskStateService(ITaskStateRepository taskState)
    {
        _taskState = taskState;
    }

    public async Task<IEnumerable<TaskStateModel>> GetAllAsync()
    {
        var states = await _taskState.GetAllStatesAsync();

        var StateList = states.Select(state => new TaskStateModel
        {
            Id = state.Id,
            Name = state.Name,
        });
        
        return StateList;
    }
}