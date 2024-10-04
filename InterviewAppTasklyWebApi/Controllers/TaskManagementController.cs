using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewAppTasklyWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/management")]
public class TaskManagementController : ControllerBase
{
    private readonly ITaskManagementService _taskManagement;

    public TaskManagementController(ITaskManagementService taskManagement)
    {
        _taskManagement = taskManagement;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskManagementModel>>> GetAllTasks()
    {
        var tasks = await _taskManagement.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskManagementModel>> GetTaskById(int id)
    {
        var task = await _taskManagement.GetTaskByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskManagementModel>> CreateTask(TaskManagementModel task)
    {
        await _taskManagement.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskManagementModel task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        await _taskManagement.UpdateTaskAsync(task);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _taskManagement.GetTaskByIdAsync(id);
        
        if (task == null)
        {
            return NotFound();
        }

        await _taskManagement.DeleteTaskAsync(id);

        return NoContent();
    }
}