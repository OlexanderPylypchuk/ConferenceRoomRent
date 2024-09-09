using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Controllers
{
    [Route("api/conferenceroom")]
    [ApiController]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConferenceRoomRepository _conferenceRoomRepository;
        private readonly ResponceDto _responceDto;
        public ConferenceRoomController(IMapper mapper, ConferenceRoomRepository conferenceRoomRepository)
        {
            _mapper = mapper;
            _conferenceRoomRepository = conferenceRoomRepository;
            _responceDto = new ResponceDto();
        }
        [HttpGet("{id:int}")]
        public async Task<ResponceDto> Get(int id)
        {
            try
            {
                var conferenceRoom = await _conferenceRoomRepository.GetAsync(u=>u.Id == id);
                if(conferenceRoom == null)
                {
                    throw new Exception("No such entity in database");
                }
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomDto>(conferenceRoom);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpGet]
        public async Task<ResponceDto> Get([FromQuery]int pageSize=3, int pageNumber=1)
        {
            try
            {
                var list = await (await _conferenceRoomRepository.GetAllAsync(pageSize:pageSize, pageNumber:pageNumber)).ToListAsync();
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<List<ConferenceRoomDto>>(list);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpPost]
        public async Task<ResponceDto> Create([FromBody]ConferenceRoomDto conferenceRoomDto)
        {
            try
            {
                var conferenceRoom = _mapper.Map<ConferenceRoom>(conferenceRoomDto);
                await _conferenceRoomRepository.CreateAsync(conferenceRoom);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomDto>(conferenceRoom);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpPut]
        public async Task<ResponceDto> Update([FromBody]ConferenceRoomDto conferenceRoomDto)
        {
            try
            {
                var conferenceRoom = _mapper.Map<ConferenceRoom>(conferenceRoomDto);
                await _conferenceRoomRepository.UpdateAsync(conferenceRoom);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomDto>(conferenceRoom);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpDelete("{id:int}")]
        public async Task<ResponceDto> Delete(int id)
        {
            try
            {
                var conferenceRoom = await _conferenceRoomRepository.GetAsync(u=>u.Id == id);
                await _conferenceRoomRepository.DeleteAsync(conferenceRoom);
                _responceDto.Success = true;
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
    }
}
