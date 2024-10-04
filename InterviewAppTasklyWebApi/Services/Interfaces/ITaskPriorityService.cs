using InterviewAppTasklyWebApi.Models;

namespace InterviewAppTasklyWebApi.Services.Interfaces;

public interface ITaskPriorityService
{
    Task<IEnumerable<TaskPriorityModel>> GetAllAsync();
}