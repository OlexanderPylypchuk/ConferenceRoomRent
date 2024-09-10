using ConferenceRoomRentAPI.Data;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace ConferenceRoomRentAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            UtilityRepository = new UtilityRepository(_db);
            RentRepository = new ConferenceRoomRentRepository(_db);
            ConferenceRoomRepository = new ConferenceRoomRepository(_db);
        }

        public IUtilityRepository UtilityRepository {get; private set;}
        public IConferenceRoomRentRepository RentRepository { get; private set; }
        public IConferenceRoomRepository ConferenceRoomRepository { get; private set; }
    }
}
