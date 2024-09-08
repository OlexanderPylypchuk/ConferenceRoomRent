namespace ConferenceRoomRentAPI.Models.Dtos
{
    public class LoginResponceDto
    {
        public UserDto AppUser { get; set; }
        public string Token { get; set; }
    }
}
