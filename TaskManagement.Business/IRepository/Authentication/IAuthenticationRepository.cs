using TaskManagement.Models.Models.DTO;
using TaskManagement.Models.Models.Responses;

namespace TaskManagement.Business.IRepository.Authentication
{
    public interface IAuthenticationRepository
    {
        string GenerateToken();
        Task<LoginResponse> Login(LoginDTO payload);
    }
}
