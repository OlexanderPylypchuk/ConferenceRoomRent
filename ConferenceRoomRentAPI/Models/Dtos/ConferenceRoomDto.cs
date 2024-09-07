using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomRentAPI.Models.Dtos
{
    public class ConferenceRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(10, 500)]
        public int PeopleCapacity { get; set; }
        public double PricePerHour { get; set; }
    }
}
