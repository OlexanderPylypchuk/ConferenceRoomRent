using System.Reflection.PortableExecutable;
using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;

namespace ConferenceRoomRentAPI.MapperConfig
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<ConferenceRoom, ConferenceRoomDto>().ReverseMap();
                config.CreateMap<ConferenceRoomRent, ConferenceRoomRentDto>().ReverseMap();
                config.CreateMap<Utilities, UtilitiesDto>().ReverseMap();
            });
            return mapping;
        }
    }
}
