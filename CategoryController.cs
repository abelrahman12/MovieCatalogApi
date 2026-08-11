using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviCatalogApi.Dtos.CategoryDto;
using MoviCatalogApi.Models;

namespace MoviCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _Context ;
        public CategoryController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var  Categorys = _Context.Categorys.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }) .ToList();
            if (Categorys.Count == 0)
            {

                return NotFound("No Category Found");
            }
            return Ok(Categorys);
        }
        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _Context.Categorys.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            var dto = new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(dto);
        }
        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (dto.Name == null)
            {
                return BadRequest("Category name is required.");
            }

            var category = new Category
            {
                Name = dto.Name
            };

            _Context.Categorys.Add(category);
            _Context.SaveChanges();

            return Ok("Created Category Successfuly");
        }
        [HttpPut("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var category = _Context.Categorys.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            category.Name = dto.Name;
            _Context.SaveChanges();

            return Ok("Updated Successfully");
        }
        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _Context.Categorys.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            _Context.Categorys.Remove(category);
            _Context.SaveChanges();

            return Ok("Deleted Successfully");
        }
    }
}
