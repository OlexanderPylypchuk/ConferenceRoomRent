using ConferenceRoomRentAPI.Models;

namespace ConferenceRoomRentAPI.Repository.IRepository
{
    public interface IUtilityRepository:IRepository<Utility>
    {
        Task UpdateAsync(Utility entity);
    }
}
