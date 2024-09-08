using ConferenceRoomRentAPI.Data;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Repository.IRepository;

namespace ConferenceRoomRentAPI.Repository
{
    public class ConferenceRoomRepository : Repository<ConferenceRoom>, IConferenceRoomRepository
    {
        private readonly AppDbContext _appDbContext;
        public ConferenceRoomRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task UpdateAsync(ConferenceRoom conferenceRoom)
        {
            _appDbContext.ConferenceRooms.Update(conferenceRoom);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
