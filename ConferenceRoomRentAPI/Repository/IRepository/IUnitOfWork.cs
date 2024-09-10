namespace ConferenceRoomRentAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUtilityRepository UtilityRepository { get; }
        IConferenceRoomRentRepository RentRepository { get; }
        IConferenceRoomRepository ConferenceRoomRepository { get; }
    }
}
