using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Repository;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Controllers
{
    [Route("api/rent")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IConferenceRoomRentRepository _conferenceRoomRentRepository;
        private readonly IMapper _mapper;
        private ResponceDto _responceDto;
        public RentController(IMapper mapper, IConferenceRoomRentRepository conferenceRoomRentRepository)
        {
            _mapper = mapper;
            _conferenceRoomRentRepository = conferenceRoomRentRepository;
            _responceDto = new ResponceDto();
        }
        [HttpGet("{id:int}")]
        public async Task<ResponceDto> Get(int id)
        {
            try
            {
                var rent = await _conferenceRoomRentRepository.GetAsync(u=>u.Id == id);
                if(rent == null)
                {
                    throw new Exception("No such entity in database");
                }
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomRentDto>(rent);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpGet]
        public async Task<ResponceDto> Get([FromQuery] int pageSize = 3, int pageNumber = 1)
        {
            try
            {
                var list = await (await _conferenceRoomRentRepository.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber)).ToListAsync();
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<List<ConferenceRoomRentDto>>(list);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpPost]
        public async Task<ResponceDto> Create([FromBody] ConferenceRoomRentDto rentDto)
        {
            try
            {
                var rent = _mapper.Map<ConferenceRoomRent>(rentDto);
                await _conferenceRoomRentRepository.CreateAsync(rent);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomDto>(rent);
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpPut]
        public async Task<ResponceDto> Update([FromBody] ConferenceRoomRentDto rentDto)
        {
            try
            {
                var rent = _mapper.Map<ConferenceRoomRent>(rentDto);
                await _conferenceRoomRentRepository.UpdateAsync(rent);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<ConferenceRoomRentDto>(rent);
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
                var rent = await _conferenceRoomRentRepository.GetAsync(u => u.Id == id);
                await _conferenceRoomRentRepository.DeleteAsync(rent);
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
