using Microsoft.AspNetCore.Identity;

namespace ConferenceRoomRentAPI.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
