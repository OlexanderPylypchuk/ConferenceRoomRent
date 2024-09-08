using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomRentAPI.Models.Dtos
{
    public class ConferenceRoomRentDto
    {
        public int Id { get; set; }
        public double FullPrice { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        [ForeignKey(nameof(ConferenceRoom))]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoomDto ConferenceRoom { get; set;}
        public List<Utility> Utilities { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
