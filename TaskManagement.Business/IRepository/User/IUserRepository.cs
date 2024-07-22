using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Payload.Filters;
using TaskManagement.Business.Models.Responses;
using TaskManagement.Models.Models.DTO;

namespace TaskManagement.Business.IRepository.User
{
    public interface IUserRepository
    {
        PaginatorResponse<UserDto> GetUserPaginated(PaginatorPayload<UserFilter> payload);
        Task<KeyValueResponse> UpdateUser(UpdateUserPayload payload);
        Task<KeyValueResponse> CreateUser(CreateUserPayload payload);
        Task<KeyValueResponse> DeleteUser(int payload);

    }
}
