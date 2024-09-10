using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Controllers
{
    [Route("api/utilities")]
    [ApiController]
    [Authorize]
    public class UtilityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ResponceDto _responceDto;
        public UtilityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responceDto = new ResponceDto();
        }
        [HttpGet("{id:int}")]
        public async Task<ResponceDto> Get(int id)
        {
            try
            {
                var utility = await _unitOfWork.UtilityRepository.GetAsync(u => u.Id == id);
                if (utility == null)
                {
                    throw new Exception("No such entity in database");
                }
                _responceDto.Success = true;
                _responceDto.Result = utility;
            }
            catch (Exception ex)
            {
                _responceDto.Success = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
        [HttpGet]
        public async Task<ResponceDto> GetAll([FromQuery] int pageSize = 3, int pageNumber = 1)
        {
            try
            {
                var list = await (await _unitOfWork.UtilityRepository.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber)).ToListAsync();
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<List<UtilityDto>>(list);
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
        public async Task<ResponceDto> Create([FromBody] UtilityDto utilityDto)
        {
            try
            {
                var utility = _mapper.Map<Utility>(utilityDto);
                await _unitOfWork.UtilityRepository.CreateAsync(utility);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<UtilityDto>(utility);
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
        public async Task<ResponceDto> Update([FromBody] UtilityDto utilityDto)
        {
            try
            {
                var utility = _mapper.Map<Utility>(utilityDto);
                await _unitOfWork.UtilityRepository.UpdateAsync(utility);
                _responceDto.Success = true;
                _responceDto.Result = _mapper.Map<UtilityDto>(utility);
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
                var utility = await _unitOfWork.UtilityRepository.GetAsync(u=>u.Id == id);
                if (utility == null)
                {
                    throw new Exception("No such entity in database");
                }
                await _unitOfWork.UtilityRepository.DeleteAsync(utility);
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
