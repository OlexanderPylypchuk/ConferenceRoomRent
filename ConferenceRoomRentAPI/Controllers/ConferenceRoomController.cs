using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Repository;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConferenceRoomRentAPI.Controllers
{
    [Route("api/conferenceroom")]
    [ApiController]
    [Authorize]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponceDto _responceDto;
        public ConferenceRoomController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responceDto = new ResponceDto();
        }
        [HttpGet("{id:int}")]
        public async Task<ResponceDto> Get(int id)
        {
            try
            {
                var conferenceRoom = await _unitOfWork.ConferenceRoomRepository.GetAsync(u=>u.Id == id);
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
                var list = await (await _unitOfWork.ConferenceRoomRepository.GetAllAsync(pageSize:pageSize, pageNumber:pageNumber)).ToListAsync();
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
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<ResponceDto> Create([FromBody]ConferenceRoomDto conferenceRoomDto)
        {
            try
            {
                var conferenceRoom = _mapper.Map<ConferenceRoom>(conferenceRoomDto);
                await _unitOfWork.ConferenceRoomRepository.CreateAsync(conferenceRoom);
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
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<ResponceDto> Update([FromBody]ConferenceRoomDto conferenceRoomDto)
        {
            try
            {
                var conferenceRoom = _mapper.Map<ConferenceRoom>(conferenceRoomDto);
                await _unitOfWork.ConferenceRoomRepository.UpdateAsync(conferenceRoom);
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
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<ResponceDto> Delete(int id)
        {
            try
            {
                var conferenceRoom = await _unitOfWork.ConferenceRoomRepository.GetAsync(u=>u.Id == id);
                if (conferenceRoom == null)
                {
                    throw new Exception("No such entity in database");
                }
                await _unitOfWork.ConferenceRoomRepository.DeleteAsync(conferenceRoom);
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
