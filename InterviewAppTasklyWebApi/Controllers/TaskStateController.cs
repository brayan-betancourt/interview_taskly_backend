using InterviewAppTasklyWebApi.Models;
using InterviewAppTasklyWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewAppTasklyWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/states")]
public class TaskStateController : ControllerBase
{
    private readonly ITaskStateService _taskState;

    public TaskStateController(ITaskStateService taskState)
    {
        _taskState = taskState;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskStateModel>>> GetAllStates()
    {
        var tasks = await _taskState.GetAllAsync();
        return Ok(tasks);
    }
}