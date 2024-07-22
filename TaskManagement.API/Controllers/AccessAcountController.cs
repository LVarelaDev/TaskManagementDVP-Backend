using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Helpers;
using TaskManagement.Business.IRepository.Authentication;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Models.Models.DTO;
using TaskManagement.Models.Models.Responses;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessAcountController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;


        public AccessAcountController(IAuthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginPayload payload)
        {
            CryptoHelper crypto = new CryptoHelper(_configuration);
            LoginDTO loginDTO = crypto.Decrypt<LoginDTO>(payload.Value);

            LoginResponse response = await _authenticationRepository.Login(loginDTO);

            return Ok(response);
        }
    }
}
