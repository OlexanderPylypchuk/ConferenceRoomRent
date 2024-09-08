using ConferenceRoomRentAPI.Data;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Repository.IRepository;

namespace ConferenceRoomRentAPI.Repository
{
    public class ConferenceRoomRentRepository:Repository<ConferenceRoomRent>, IConferenceRoomRentRepository
    {
        private readonly AppDbContext _db;
        public ConferenceRoomRentRepository(AppDbContext db):base(db) 
        {
            _db = db;
        }

        public async Task UpdateAsync(ConferenceRoomRent conferenceRoomRent)
        {
            _db.Update(conferenceRoomRent);
            await _db.SaveChangesAsync();
        }
    }
}
