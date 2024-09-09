using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomRentAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponceDto _responceDto;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _responceDto = new ResponceDto();

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responceDto.Success = false;
                _responceDto.Message = errorMessage;
                return BadRequest(_responceDto);
            }
            _responceDto.Success = true;
            return Ok(_responceDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginresponce = await _authService.Login(loginRequestDto);
            if (loginresponce.AppUser == null)
            {
                _responceDto.Success = false;
                _responceDto.Message = "Username or password is incorrect";
                return BadRequest(_responceDto);
            }
            _responceDto.Success = true;
            _responceDto.Result = loginresponce;
            return Ok(_responceDto);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var assignRoleSuccessful = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responceDto.Success = false;
                _responceDto.Message = "Error occured";
                return BadRequest(_responceDto);
            }
            _responceDto.Success = true;
            return Ok(_responceDto);
        }
    }
}
