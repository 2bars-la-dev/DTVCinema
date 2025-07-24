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
    //[Authorize(Policy = Constant.AuthPolicy_ManagerAndAbove)]
    public class TheatreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TheatreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/theatre
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var theatres = await _unitOfWork.Theatre.GetAllAsync(includeProperties: "Province");
            var result = _mapper.Map<IEnumerable<TheatreGetDto>>(theatres);
            return Ok(result);
        }

        // GET: api/theatre/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var theatre = await _unitOfWork.Theatre.GetAsync(t => t.Id == id, includeProperties: "Province");
            if (theatre == null) return NotFound();

            var result = _mapper.Map<TheatreGetDto>(theatre);
            return Ok(result);
        }

        // POST: api/theatre
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TheatreUpcrateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var theatre = _mapper.Map<Theatre>(dto);
            await _unitOfWork.Theatre.AddAsync(theatre);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<TheatreGetDto>(theatre);
            return CreatedAtAction(nameof(GetById), new { id = theatre.Id }, result);
        }

        // PUT: api/theatre/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TheatreUpcrateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var theatreFromDb = await _unitOfWork.Theatre.GetAsync(t => t.Id == id);
            if (theatreFromDb == null) return NotFound();

            _mapper.Map(dto, theatreFromDb);
            theatreFromDb.UpdatedAt = DateTime.Now;

            _unitOfWork.Theatre.Edit(theatreFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/theatre/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var theatre = await _unitOfWork.Theatre.GetAsync(t => t.Id == id);
            if (theatre == null) return NotFound();

            _unitOfWork.Theatre.Remove(theatre);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // POST: api/theatre/toggle-status/5
        [HttpPost("toggle-status/{id:int}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var theatre = await _unitOfWork.Theatre.GetAsync(t => t.Id == id);
            if (theatre == null) return NotFound();

            theatre.IsActive = !theatre.IsActive;
            theatre.UpdatedAt = DateTime.Now;

            _unitOfWork.Theatre.Edit(theatre);
            await _unitOfWork.SaveAsync();

            return Ok(new { status = theatre.IsActive });
        }
    }
}
