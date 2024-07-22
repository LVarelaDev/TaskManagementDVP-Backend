using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Business.IRepository.User;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Payload.Filters;
using TaskManagement.Business.Models.Responses;
using TaskManagement.Models.Models.DTO;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("GetUserPaginated")]
        public ActionResult<PaginatorResponse<UserDto>> GetUserPaginated([FromBody] PaginatorPayload<UserFilter> payload)
        {
            return Ok(_userRepository.GetUserPaginated(payload));
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<KeyValueResponse>> CreateUser([FromBody] CreateUserPayload payload)
        {
            return Ok(await _userRepository.CreateUser(payload));
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<KeyValueResponse>> UpdateUser([FromBody] UpdateUserPayload payload)
        {
            return Ok(await _userRepository.UpdateUser(payload));
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<KeyValueResponse>> DeleteUser(int id)
        {
            return Ok(await _userRepository.DeleteUser(id));
        }
    }
}
