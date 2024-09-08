using ConferenceRoomRentAPI.Models.Dtos;

namespace ConferenceRoomRentAPI.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponceDto> Login(LoginRequestDto loginRequest);
        Task<string> Register(RegistrationRequestDto registrationRequest);
        Task<bool> AssignRole(string email, string role);
    }
}
