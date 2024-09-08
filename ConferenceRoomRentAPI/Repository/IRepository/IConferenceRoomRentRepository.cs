using ConferenceRoomRentAPI.Models;

namespace ConferenceRoomRentAPI.Repository.IRepository
{
    public interface IConferenceRoomRentRepository:IRepository<ConferenceRoomRent>
    {
        Task UpdateAsync(ConferenceRoomRent conferenceRoomRent);
    }
}
