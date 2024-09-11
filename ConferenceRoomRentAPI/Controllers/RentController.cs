using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ConferenceRoomRentAPI.Models;
using ConferenceRoomRentAPI.Models.Dtos;
using ConferenceRoomRentAPI.Repository;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Controllers
{
    [Authorize]
    [Route("api/rent")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ResponceDto _responceDto;
        public RentController(IMapper mapper, IUnitOfWork unitOfWork)
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
                var rent = await _unitOfWork.RentRepository.GetAsync(u=>u.Id == id, includeProperties: "Utilities,ConferenceRoom");
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
                var list = await (await _unitOfWork.RentRepository
                    .GetAllAsync(includeProperties: "Utilities,ConferenceRoom",pageSize: pageSize, pageNumber: pageNumber))
                    .ToListAsync();
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
                rent.UserId = User.FindFirst(u => u.Type == SD.IdClaimName).Value;
                if (rent.StartOfRent >= rent.EndOfRent)
                {
                    throw new Exception("End of rent can not happen before start of rent");
                }
                var conferenceRoom = await _unitOfWork.ConferenceRoomRepository.GetAsync(u=>u.Id== rent.ConferenceRoomId);
                if(conferenceRoom == null)
                {
                    throw new Exception("No record of conference room with such id");
                }
                rent.ConferenceRoom = conferenceRoom;
                rent.FullPrice = TotalPriceCalculation(rent.StartOfRent, rent.EndOfRent, conferenceRoom.PricePerHour);

                for (int i=0; i<rent.Utilities.Count; i++)
                {
                    var item = await _unitOfWork.UtilityRepository.GetAsync(u=>u.Id == rent.Utilities[i].Id);
                    if(item != null)
                    {
                        rent.Utilities[i] = item;
                        rent.FullPrice += item.Price;
                    }
                    else
                    {
                        rent.Utilities.RemoveAt(i);
                        i--;
                    }
                }

                await _unitOfWork.RentRepository.CreateAsync(rent);
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
        [HttpPut]
        public async Task<ResponceDto> Update([FromBody] ConferenceRoomRentDto rentDto)
        {
            try
            {
                var rent = await _unitOfWork.RentRepository.GetAsync(r => r.Id == rentDto.Id, "Utilities");
                if (rent == null)
                {
                    throw new Exception("ConferenceRoomRent not found.");
                }

                var currentUserId = User.FindFirst(u => u.Type == SD.IdClaimName)?.Value;
                if (rent.UserId != currentUserId && !User.IsInRole(SD.RoleAdmin))
                {
                    throw new Exception("Access denied.");
                }

                if (rentDto.StartOfRent >= rentDto.EndOfRent)
                {
                    throw new Exception("End of rent cannot occur before the start of rent.");
                }

                var conferenceRoom = await _unitOfWork.ConferenceRoomRepository.GetAsync(u => u.Id == rentDto.ConferenceRoomId);
                if (conferenceRoom == null)
                {
                    throw new Exception("No record of conference room with the provided ID.");
                }

                rent.ConferenceRoomId = conferenceRoom.Id;
                rent.StartOfRent = rentDto.StartOfRent;
                rent.EndOfRent = rentDto.EndOfRent;
                rent.FullPrice = TotalPriceCalculation(rent.StartOfRent, rent.EndOfRent, conferenceRoom.PricePerHour);

                rent.Utilities.Clear();

                foreach (var utilityDto in rentDto.Utilities)
                {
                    var utility = await _unitOfWork.UtilityRepository.GetAsync(u=>u.Id==utilityDto.Id);
                    if (utility != null)
                    {
                        rent.Utilities.Add(utility);
                        rent.FullPrice += utility.Price;
                    }
                }

                await _unitOfWork.RentRepository.UpdateAsync(rent);

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
        private double TotalPriceCalculation(DateTime start, DateTime end, double pricePerHour)
        {
            double total = 0;

            while (start < end)
            {
                double currentHourPrice = pricePerHour;

                // Стандартні години (з 09:00 до 18:00): базова вартість
                if (start.Hour >= 9 && start.Hour < 18)
                {
                    currentHourPrice = pricePerHour;
                }
                // Вечірні години (з 18:00 до 23:00): знижка 20%
                else if (start.Hour >= 18 && start.Hour < 23)
                {
                    currentHourPrice = pricePerHour * 0.8;
                }
                // Ранкові години (з 06:00 до 09:00): знижка 10%
                else if (start.Hour >= 6 && start.Hour < 9)
                {
                    currentHourPrice = pricePerHour * 0.9;
                }
                // Пікові години (з 12:00 до 14:00): націнка 15%
                else if (start.Hour >= 12 && start.Hour < 14)
                {
                    currentHourPrice = pricePerHour * 1.15;
                }

                total += currentHourPrice;

                // Переходимо до наступної години
                start = start.AddHours(1);
            }

            return total;
        }
        [HttpDelete("{id:int}")]
        public async Task<ResponceDto> Delete(int id)
        {
            try
            {
                var rent = await _unitOfWork.RentRepository.GetAsync(u => u.Id == id);
                var currentUserId = User.FindFirst(u => u.Type == SD.IdClaimName)?.Value;
                if (rent.UserId != currentUserId && !User.IsInRole(SD.RoleAdmin))
                {
                    throw new Exception("Access denied.");
                }
                await _unitOfWork.RentRepository.DeleteAsync(rent);
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
