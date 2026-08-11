using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviCatalogApi.Dtos.MoviDto;
using MoviCatalogApi.Models;
using Microsoft.EntityFrameworkCore;
namespace MoviCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;
        public MoviController(ApplicationDbContext context)
        {
            _Context = context;
        }
        [HttpGet("GetAllMovies")]
        public IActionResult GetAllMovi()
        {
            var Movi = _Context.Movis.Include(c => c.Category).Select(m => new GetMoviDto
            {
                Id = m.Id,
                Title = m.Title,
                Rating = m.Rating,
                CategoryName = m.Category.Name
            }).ToList();
            if (Movi.Count == 0)
            {
                return NotFound("NoMovies");
            }

            return Ok(Movi);
        }
        [HttpGet("GetMovieById/{id}")]
        public IActionResult GetMovie(int id)
        {
            var movie = _Context.Movis.Include(c => c.Category)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            var dto = new GetMoviDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                CategoryName = movie.Category.Name
            };

            return Ok(dto);
        }
        [HttpPost("CreateMovie")]
        public IActionResult CreateMovi([FromBody] CreatMoviDto dto)
        {
            var category = _Context.Categorys
                .FirstOrDefault(c => c.Id == dto.CategoryId);

            if (category == null)
            {
                return NotFound($"Category with ID {dto.CategoryId} not found.");
            }

            var movie = new Movi
            {
                Title = dto.Title,
                Director = dto.Director,
                ReleaseYear = dto.ReleaseYear,
                CategoryId = dto.CategoryId,
                Rating = dto.Rating
            };

            _Context.Movis.Add(movie);
            _Context.SaveChanges();

            return Ok("Movie created successfully.");
        }
        [HttpPut("UpdateMovie/{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMoviDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var movie = _Context.Movis.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            var category = _Context.Categorys.FirstOrDefault(c => c.Id == dto.CategoryId);
            if (category == null)
            {
                return NotFound($"Category with ID {dto.CategoryId} not found.");
            }

            movie.Title = dto.Title;
            movie.Rating = dto.Rating;
            movie.CategoryId = dto.CategoryId;
            movie.Director = dto.Director;

            _Context.SaveChanges();

            return Ok("Movie updated successfully.");
        }
        [HttpDelete("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _Context.Movis.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            _Context.Movis.Remove(movie);
            _Context.SaveChanges();

            return Ok("Movie deleted successfully.");
        }
    }
}
