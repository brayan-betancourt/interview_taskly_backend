using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewAppTasklyWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/priorities")]
public class TaskPriorityController : ControllerBase
{
    private readonly ITaskPriorityService _taskPriority;

    public TaskPriorityController(ITaskPriorityService taskPriority)
    {
        _taskPriority = taskPriority;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskPriorityModel>>> GetAllPriorities()
    {
        var tasks = await _taskPriority.GetAllAsync();
        return Ok(tasks);
    }
}