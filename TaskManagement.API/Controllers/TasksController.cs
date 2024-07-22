using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Business.IRepository.Task;
using TaskManagement.Business.Models.DTO;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Responses;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        [Route("GetEmployes")]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _taskRepository.GetUsersForSelect());
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public async Task<ActionResult<List<TaskResponse>>> GetAllTasks()
        {
            return Ok(await _taskRepository.GetAll());
        }

        [HttpPost]
        [Route("CreateTask")]
        public async Task<ActionResult<KeyValueResponse>> CreateTask([FromBody] TaskDTO payload)
        {
            return Ok(await _taskRepository.CreateTask(payload));
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<ActionResult<KeyValueResponse>> UpdateTask([FromBody] UpdateTaskPayload payload)
        {
            return Ok(await _taskRepository.UpdateTask(payload));
        }

        [HttpPut]
        [Route("UpdateStatusTask")]
        public async Task<ActionResult<KeyValueResponse>> UpdateStatus([FromBody] UpdateStatus payload)
        {
            return Ok(await _taskRepository.UpdateStatus(payload));
        }

        [HttpGet]
        [Route("GetTaskByCodeAndTitle")]
        public async Task<ActionResult<List<TaskResponse>>> GetTaskByCodeAndTitle(string searchValue)
        {
            return Ok(await _taskRepository.GetTaskByCodeAndTitle(searchValue));
        }
    }
}
