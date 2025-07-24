using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace MovieTheatreWeb.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(Policy = Constant.AuthPolicy_ManagerAndAbove)]
    public class ScreenController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScreenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("screens")]
        public async Task<IActionResult> GetAll([FromQuery] int? theatreId)
        {
            var screens = await _unitOfWork.Screen.GetAllAsync(
                s => !theatreId.HasValue || s.TheatreId == theatreId,
                includeProperties: "Theatre"
            );
            var result = _mapper.Map<IEnumerable<ScreenGetDto>>(screens);
            return Ok(result);
        }

        [HttpGet("screens/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var screen = await _unitOfWork.Screen.GetAsync(
                s => s.Id == id,
                includeProperties: "Theatre,Seats"
            );
            if (screen == null)
                return NotFound();

            return Ok(_mapper.Map<ScreenGetDto>(screen));
        }

        [HttpPost("screens")]
        public async Task<IActionResult> Create([FromBody] ScreenUpcrateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var screen = _mapper.Map<Screen>(dto);
            screen.Capacity = screen.RowNum * screen.ColNum;

            await _unitOfWork.Screen.AddAsync(screen);
            await _unitOfWork.SaveAsync();

            await GenerateSeatsAsync(screen.Id, screen.RowNum, screen.ColNum);

            return Ok(new { message = "Screen created successfully" });
        }

        [HttpPut("screens/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScreenUpcrateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _unitOfWork.Screen.GetAsync(s => s.Id == id);
            if (existing == null)
                return NotFound();

            // Kiểm tra nếu hàng/cột thay đổi thì xóa & tạo lại ghế
            if (existing.RowNum != dto.RowNum || existing.ColNum != dto.ColNum)
            {
                var oldSeats = await _unitOfWork.Seat.GetAllAsync(s => s.ScreenId == id);
                _unitOfWork.Seat.RemoveRange(oldSeats);
                await GenerateSeatsAsync(id, dto.RowNum, dto.ColNum);
            }

            _mapper.Map(dto, existing);
            existing.Capacity = dto.RowNum * dto.ColNum;

            _unitOfWork.Screen.Edit(existing);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Screen updated successfully" });
        }

        [HttpPost("screens/{id}/toggle-active")]
        public async Task<IActionResult> ToggleScreenActive(int id)
        {
            var screen = await _unitOfWork.Screen.GetAsync(s => s.Id == id);
            if (screen == null)
                return NotFound();

            screen.IsActive = !screen.IsActive;
            _unitOfWork.Screen.Edit(screen);
            await _unitOfWork.SaveAsync();

            return Ok(new { isActive = screen.IsActive });
        }

        [HttpPost("seats/{id}/toggle-active")]
        public async Task<IActionResult> ToggleSeatActive(int id)
        {
            var seat = await _unitOfWork.Seat.GetAsync(s => s.Id == id);
            if (seat == null)
                return NotFound(new { message = "Seat not found" });

            seat.IsActive = !seat.IsActive;
            _unitOfWork.Seat.Edit(seat);
            await _unitOfWork.SaveAsync();

            return Ok(new { isActive = seat.IsActive });
        }

        [HttpGet("screens/{id}/seats")]
        public async Task<IActionResult> GetSeats(int id)
        {
            var seats = await _unitOfWork.Seat.GetAllAsync(s => s.ScreenId == id);
            return Ok(seats);
        }

        private async Task GenerateSeatsAsync(int screenId, int rowNum, int colNum)
        {
            var seats = new List<Seat>();
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 1; j <= colNum; j++)
                {
                    seats.Add(new Seat
                    {
                        ScreenId = screenId,
                        Row = i,
                        Number = j
                    });
                }
            }

            await _unitOfWork.Seat.AddRangeAsync(seats);
            await _unitOfWork.SaveAsync();
        }
    }
}
