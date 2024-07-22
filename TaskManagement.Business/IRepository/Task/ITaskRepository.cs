using TaskManagement.Business.Models.DTO;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Responses;

namespace TaskManagement.Business.IRepository.Task
{
    public interface ITaskRepository
    {
        Task<List<UsersSelectDTO>> GetUsersForSelect();
        Task<List<TaskResponse>> GetAll();
        Task<KeyValueResponse> CreateTask(TaskDTO taskDTO);
        Task<KeyValueResponse> UpdateTask(UpdateTaskPayload taskDTO);
        Task<List<TaskResponse>> GetTaskByCodeAndTitle(string searchValue);
        Task<KeyValueResponse> UpdateStatus(UpdateStatus payload);

    }
}
