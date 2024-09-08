using ConferenceRoomRentAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions) 
        {
            
        }
        public DbSet<ConferenceRoomRent> ConferenceRoomRents { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ConferenceRoom>().HasData(new[]{
                new ConferenceRoom() { Id=1,Name = "A", PeopleCapacity=50, PricePerHour=2000},
                new ConferenceRoom() { Id=2,Name="B", PeopleCapacity=100, PricePerHour=3500},
                new ConferenceRoom() { Id=3,Name="C",PeopleCapacity=30, PricePerHour=1500}
            });
            builder.Entity<Utility>().HasData(new[]
            {
                new Utility() { Id=1, Name="Проєктор", Price=500},
                new Utility() { Id=2, Name="WiFi", Price=300},
                new Utility() { Id=3, Name="Звук", Price=700}
            });
        }
    }
}
