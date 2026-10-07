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
    public class ProductsController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(
     string? search,
     int? categoryId,
     int? supplierId,
     int page = 1,
     int pageSize = 10)
        {
            var query = _context.Products
                .Include(e => e.Category)
                .Include(e => e.Supplier)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.Name.Contains(search) ||
                    (e.Description != null && e.Description.Contains(search)));
            }

            // Category filter
            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            // Supplier filter
            if (supplierId.HasValue)
            {
                query = query.Where(e => e.SupplierId == supplierId.Value);
            }

            // Pagination validation
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            var totalItems = query.Count();

            var products = query
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new ProductDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Price = e.Price,
                    CategoryId = e.CategoryId,
                    CategoryName = e.Category.Name,
                    SupplierId = e.SupplierId,
                    SupplierName = e.Supplier.Name,
                    CreatedAt = e.CreatedAt
                })
                .ToList();

            return Ok(new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                data = products
            });
        } 

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Save(ProductSaveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Products.Any(e => e.Id == dto.Id) == false)
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId,
                    SupplierId = dto.SupplierId,
                    CreatedAt = dto.CreatedAt
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return Ok(product);
            }
            else
            {
                var product = _context.Products.Find(dto.Id);

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.CategoryId = dto.CategoryId;
                product.SupplierId = dto.SupplierId;
                product.CreatedAt = dto.CreatedAt;

                _context.SaveChanges();

                return Ok(product);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok("Product deleted successfully");
        }
    }
}