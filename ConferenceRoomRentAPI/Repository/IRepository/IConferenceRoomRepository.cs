using ConferenceRoomRentAPI.Models;

namespace ConferenceRoomRentAPI.Repository.IRepository
{
    public interface IConferenceRoomRepository:IRepository<ConferenceRoom>
    {
        Task UpdateAsync(ConferenceRoom conferenceRoom);
    }
}
