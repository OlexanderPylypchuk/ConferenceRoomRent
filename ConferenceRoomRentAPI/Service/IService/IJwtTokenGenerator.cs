using ConferenceRoomRentAPI.Models;

namespace ConferenceRoomRentAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
