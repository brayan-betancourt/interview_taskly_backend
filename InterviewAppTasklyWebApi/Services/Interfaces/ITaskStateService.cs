using InterviewAppTasklyWebApi.Models;

namespace InterviewAppTasklyWebApi.Services.Interfaces;

public interface ITaskStateService
{
    Task<IEnumerable<TaskStateModel>> GetAllAsync();
}