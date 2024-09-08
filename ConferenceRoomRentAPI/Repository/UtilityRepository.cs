using ConferenceRoomRentAPI.Data;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Repository.IRepository;

namespace ConferenceRoomRentAPI.Repository
{
    public class UtilityRepository : Repository<Utility>, IUtilityRepository
    {
        private readonly AppDbContext _appDbContext;
        public UtilityRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task UpdateAsync(Utility entity)
        {
            _appDbContext.Utilities.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
