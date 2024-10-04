using InterviewAppTasklyWebApi.Entities;

namespace InterviewAppTasklyWebApi.Repositories.Interfaces;

public interface ITaskStateRepository
{
    Task<IEnumerable<TaskState>> GetAllStatesAsync();
}