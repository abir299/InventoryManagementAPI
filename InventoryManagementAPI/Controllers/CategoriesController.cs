using InventoryManagementAPI.Data;
using InventoryManagementAPI.Data.Entities;
using InventoryManagementAPI.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class CategoriesController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories
                .Include(e => e.Products)
                .Select(e => new CategoryDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreatedAt = e.CreatedAt,
                    ProductCount = e.Products.Count
                })
                .ToList();

            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories
                .Include(e => e.Products)
                .Where(e => e.Id == id)
                .Select(e => new CategoryDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreatedAt = e.CreatedAt,
                    ProductCount = e.Products.Count
                })
                .FirstOrDefault();

            if (category == null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Save(CategorySaveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Categories.Any(e => e.Id == dto.Id) == false)
            {
                var category = new Category
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CreatedAt = dto.CreatedAt
                };

                _context.Categories.Add(category);
                _context.SaveChanges();

                return Ok(category);
            }

            var existingCategory = _context.Categories.Find(dto.Id);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            existingCategory.Name = dto.Name;
            existingCategory.Description = dto.Description;
            existingCategory.CreatedAt = dto.CreatedAt;

            _context.SaveChanges();

            return Ok(existingCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok("Category deleted successfully");
        }
    }
}