using InterviewAppTasklyWebApi.Models;

namespace InterviewAppTasklyWebApi.Services.Interfaces;

public interface ITaskManagementService
{
    Task<IEnumerable<TaskManagementModel>> GetAllTasksAsync();
    Task<TaskManagementModel> GetTaskByIdAsync(int id);
    Task AddTaskAsync(TaskManagementModel task);
    Task UpdateTaskAsync(TaskManagementModel task);
    Task DeleteTaskAsync(int id);
    Task<bool> TaskExistsAsync(int id);
}