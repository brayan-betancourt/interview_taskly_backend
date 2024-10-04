using InterviewAppTasklyWebApi.Entities;

namespace InterviewAppTasklyWebApi.Repositories.Interfaces;

public interface ITaskPriorityRepository
{
    Task<IEnumerable<TaskPriority>> GetAllAsync();
}