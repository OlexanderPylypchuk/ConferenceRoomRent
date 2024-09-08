using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ConferenceRoomRentAPI.Models
{
    public class ConferenceRoomRent
    {
        public int Id { get; set; }
        public double FullPrice { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        [ForeignKey(nameof(ConferenceRoom))]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set;}
        public List<Utility> Utilities { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [ValidateNever]
        public AppUser User { get; set; }
    }
}
