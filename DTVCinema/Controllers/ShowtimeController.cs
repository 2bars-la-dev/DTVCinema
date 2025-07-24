using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace DTVCinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShowtimeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/showtime
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? movieId, [FromQuery] int? screenId)
        {
            var showtimes = await _unitOfWork.Showtime.GetAllAsync(
                s => (!movieId.HasValue || s.MovieId == movieId.Value) &&
                     (!screenId.HasValue || s.ScreenId == screenId.Value),
                includeProperties: "Movie,Screen,Screen.Theatre"
            );

            var result = _mapper.Map<IEnumerable<ShowtimeGetDto>>(showtimes);
            return Ok(result);
        }

        // GET: api/showtime/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest();

            var showtime = await _unitOfWork.Showtime.GetAsync(
                s => s.Id == id,
                includeProperties: "Movie,Screen,Screen.Theatre"
            );

            if (showtime == null)
                return NotFound();

            var result = _mapper.Map<ShowtimeGetDto>(showtime);
            return Ok(result);
        }

        // POST: api/showtime
        [HttpPost]
        //[Authorize(Policy = Constant.AuthPolicy_StaffAndAbove)]
        public async Task<IActionResult> Create([FromBody] ShowtimeUpcrateDto showtimeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var screen = await _unitOfWork.Screen.GetAsync(s => s.Id == showtimeDto.ScreenId);
            if (screen == null)
                return BadRequest("Invalid Screen ID");

            var movie = await _unitOfWork.Movie.GetAsync(m => m.Id == showtimeDto.MovieId);
            if (movie == null)
                return BadRequest("Invalid Movie ID");

            var showtime = _mapper.Map<Showtime>(showtimeDto);
            showtime.EndTime = showtime.StartTime.AddMinutes(movie.Duration);
            showtime.IsActive = true;

            // Kiểm tra trùng suất chiếu
            var conflict = await _unitOfWork.Showtime.GetAllAsync(s =>
                s.ScreenId == showtime.ScreenId &&
                s.StartTime < showtime.EndTime &&
                s.EndTime > showtime.StartTime
            );
            if (conflict.Any())
                return BadRequest("Suất chiếu bị trùng với suất khác trong cùng phòng.");

            await _unitOfWork.Showtime.AddAsync(showtime);
            await _unitOfWork.SaveAsync();

            var result = await _unitOfWork.Showtime.GetAsync(
                s => s.Id == showtime.Id,
                includeProperties: "Movie,Screen,Screen.Theatre"
            );

            return CreatedAtAction(nameof(Get), new { id = showtime.Id }, _mapper.Map<ShowtimeGetDto>(result));
        }

        // PUT: api/showtime/5
        [HttpPut("{id}")]
        //[Authorize(Policy = Constant.AuthPolicy_StaffAndAbove)]
        public async Task<IActionResult> Update(int id, [FromBody] ShowtimeUpcrateDto showtimeDto)
        {
            if (id == 0)
                return BadRequest();

            var existingShowtime = await _unitOfWork.Showtime.GetAsync(s => s.Id == id);
            if (existingShowtime == null)
                return NotFound();

            var screen = await _unitOfWork.Screen.GetAsync(s => s.Id == showtimeDto.ScreenId);
            if (screen == null)
                return BadRequest("Invalid Screen ID");

            var movie = await _unitOfWork.Movie.GetAsync(m => m.Id == showtimeDto.MovieId);
            if (movie == null)
                return BadRequest("Invalid Movie ID");

            // Copy các giá trị từ DTO sang entity
            _mapper.Map(showtimeDto, existingShowtime);
            existingShowtime.EndTime = showtimeDto.StartTime.AddMinutes(movie.Duration);

            // Kiểm tra trùng suất chiếu (trừ chính nó)
            var conflict = await _unitOfWork.Showtime.GetAllAsync(s =>
                s.ScreenId == showtimeDto.ScreenId &&
                s.Id != id &&
                s.StartTime < existingShowtime.EndTime &&
                s.EndTime > showtimeDto.StartTime
            );
            if (conflict != null)
                return BadRequest("Suất chiếu bị trùng với suất khác trong cùng phòng.");

            _unitOfWork.Showtime.Edit(existingShowtime);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/showtime/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = Constant.AuthPolicy_StaffAndAbove)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var showtime = await _unitOfWork.Showtime.GetAsync(s => s.Id == id);
            if (showtime == null)
                return NotFound();

            _unitOfWork.Showtime.Remove(showtime);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
