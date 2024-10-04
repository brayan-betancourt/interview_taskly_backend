using InterviewAppTasklyWebApi.Entities;
using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Repositories.Interfaces;
using InterviewAppTasklyWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InterviewAppTasklyWebApi.Services.Implementations;

public class TaskManagementService : ITaskManagementService
{
    private readonly ITaskManagementRepository _taskManagement;
    private readonly ITaskPriorityRepository _taskPriority;
    private readonly ITaskStateRepository _taskState;
    private readonly UserManager<ApplicationUser> _userManager;

    public TaskManagementService(ITaskManagementRepository taskManagement, ITaskPriorityRepository taskPriority, ITaskStateRepository taskState, UserManager<ApplicationUser> userManager)
    {
        _taskManagement = taskManagement;
        _taskPriority = taskPriority;
        _taskState = taskState;
        _userManager = userManager;
    }
    
    public async Task<IEnumerable<TaskManagementModel>> GetAllTasksAsync()
    {
        var tasks = await _taskManagement.GetAllTasksAsync();
        
        var taskList = tasks.Select(task => new TaskManagementModel
        {
            Id = task.Id,
            CreationDate = task.CreationDate,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priorities?.Name,
            Status = task.States?.Name,
            CreatedBy = task.Users?.UserName,
            DueDate = task.DueDate
        }).ToList();

        return taskList;
    }

    public async Task<TaskManagementModel> GetTaskByIdAsync(int id)
    {
        var task = await _taskManagement.GetTaskByIdAsync(id);

        if (task == null)
        {
            return null;
        }
        
        var taskResult = new TaskManagementModel
        {
            Id = task.Id,
            CreationDate = task.CreationDate,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priorities?.Name,
            Status = task.States?.Name,
            CreatedBy = task.Users?.UserName,
            DueDate = task.DueDate
        };

        return taskResult;
    }

    public async Task AddTaskAsync(TaskManagementModel task)
    {
        var taskPriorities = await _taskPriority.GetAllAsync();
        var taskStates = await _taskState.GetAllStatesAsync();
        var user = await _userManager.FindByEmailAsync(task.CreatedBy);

        TaskManagement management = new TaskManagement
        {
            CreationDate = DateTime.Now,
            Title = task.Title,
            Description = task.Description,
            PriorityId = taskPriorities.Where(x => x.Name == task.Priority).Select(x => x.Id).FirstOrDefault(),
            StateId = taskStates.Where(x => x.Name == task.Status).Select(x => x.Id).FirstOrDefault(),
            UserId = user.Id,
            DueDate = task.DueDate
        };
        
        await _taskManagement.AddTaskAsync(management);
    }

    public async Task UpdateTaskAsync(TaskManagementModel taskManagement)
    {
        var task = await _taskManagement.GetTaskByIdAsync(taskManagement.Id);
        var taskPriorities = await _taskPriority.GetAllAsync();
        var taskStates = await _taskState.GetAllStatesAsync();
        
        task.Title = taskManagement.Title;
        task.Description = taskManagement.Description;
        task.StateId = taskStates.Where(x => x.Name == taskManagement.Status).Select(x => x.Id).FirstOrDefault();
        task.PriorityId = taskPriorities.Where(x => x.Name == taskManagement.Priority).Select(x => x.Id).FirstOrDefault();
        task.DueDate = taskManagement.DueDate;
        
        await _taskManagement.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskManagement.DeleteTaskAsync(id);
    }

    public async Task<bool> TaskExistsAsync(int id)
    {
        var task = await _taskManagement.GetTaskByIdAsync(id);

        if (task == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}