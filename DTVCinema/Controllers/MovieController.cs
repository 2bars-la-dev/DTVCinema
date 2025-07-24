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
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/movie
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _unitOfWork.Movie.GetAllAsync();
            var result = _mapper.Map<IEnumerable<MovieGetDto>>(movies);
            return Ok(result);
        }

        // GET: api/movie/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest();

            var movie = await _unitOfWork.Movie.GetAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            var result = _mapper.Map<MovieGetDto>(movie);
            return Ok(result);
        }

        // POST: api/movie
        [HttpPost]
        //[Authorize(Policy = Constant.AuthPolicy_StaffAndAbove)]
        public async Task<IActionResult> Create([FromForm] MovieUpcrateDto movieDto, IFormFile? posterFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _mapper.Map<Movie>(movieDto);

            if (posterFile != null)
            {
                string fileName = await SavePosterFile(posterFile);
                movie.PosterUrl = fileName;
            }

            await _unitOfWork.Movie.AddAsync(movie);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<MovieGetDto>(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, result);
        }

        // PUT: api/movie/5
        [HttpPut("{id}")]
        //[Authorize(Policy = Constant.AuthPolicy_StaffAndAbove)]
        public async Task<IActionResult> Update(int id, [FromForm] MovieUpcrateDto movieDto, IFormFile? posterFile)
        {
            if (id == 0)
                return BadRequest();

            var existingMovie = await _unitOfWork.Movie.GetAsync(m => m.Id == id);
            if (existingMovie == null)
                return NotFound();

            if (posterFile != null)
            {
                // Delete old poster if exists
                if (!string.IsNullOrEmpty(existingMovie.PosterUrl))
                {
                    var oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, 
                        existingMovie.PosterUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }

                string fileName = await SavePosterFile(posterFile);
                movieDto.PosterUrl = fileName;
            }

            _mapper.Map(movieDto, existingMovie);
            existingMovie.UpdatedAt = DateTime.Now;
            _unitOfWork.Movie.Edit(existingMovie);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/movie/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = Constant.AuthPolicy_ManagerAndAbove)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var movie = await _unitOfWork.Movie.GetAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            // Delete poster file if exists
            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                var imgPath = Path.Combine(_webHostEnvironment.WebRootPath, 
                    movie.PosterUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }

            _unitOfWork.Movie.Remove(movie);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<string> SavePosterFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string posterPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\movie");

            if (!Directory.Exists(posterPath))
            {
                Directory.CreateDirectory(posterPath);
            }

            using (var fileStream = new FileStream(Path.Combine(posterPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
