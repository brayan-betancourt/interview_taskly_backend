using InterviewAppTasklyWebApi.Entities;

namespace InterviewAppTasklyWebApi.Repositories.Interfaces;

public interface ITaskManagementRepository
{
    Task<IEnumerable<TaskManagement>> GetAllTasksAsync();
    Task<TaskManagement> GetTaskByIdAsync(int id);
    Task AddTaskAsync(TaskManagement task);
    Task UpdateTaskAsync(TaskManagement task);
    Task DeleteTaskAsync(int id);
    Task<bool> TaskExistsAsync(int id);
}