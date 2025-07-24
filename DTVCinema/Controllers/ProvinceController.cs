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
    public class ProvinceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/province
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var provinces = await _unitOfWork.Province.GetAllAsync();
            return Ok(provinces);
        }

        // GET: api/province/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var province = await _unitOfWork.Province.GetAsync(p => p.Id == id);
            if (province == null) return NotFound();
            return Ok(province);
        }

        // POST: api/province
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProvinceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var province = _mapper.Map<Province>(dto);
            await _unitOfWork.Province.AddAsync(province);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = province.Id }, province);
        }

        // POST: api/province/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] List<ProvinceDto> dtos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var provinces = dtos.Select(dto => _mapper.Map<Province>(dto)).ToList();
            await _unitOfWork.Province.AddRangeAsync(provinces);
            await _unitOfWork.SaveAsync();

            return Ok(provinces);
        }

        // PUT: api/province/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProvinceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var provinceFromDb = await _unitOfWork.Province.GetAsync(p => p.Id == id);
            if (provinceFromDb == null) return NotFound();

            _mapper.Map(dto, provinceFromDb);
            _unitOfWork.Province.Edit(provinceFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/province/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var province = await _unitOfWork.Province.GetAsync(p => p.Id == id);
            if (province == null) return NotFound();

            _unitOfWork.Province.Remove(province);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
