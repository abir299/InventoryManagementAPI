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
    public class SuppliersController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var suppliers = _context.Suppliers
                .Include(e => e.Products)
                .Select(e => new SupplierDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Address = e.Address,
                    CreatedAt = e.CreatedAt,
                    ProductCount = e.Products.Count
                })
                .ToList();

            return Ok(suppliers);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var supplier = _context.Suppliers
                .Include(e => e.Products)
                .Where(e => e.Id == id)
                .Select(e => new SupplierDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Address = e.Address,
                    CreatedAt = e.CreatedAt,
                    ProductCount = e.Products.Count
                })
                .FirstOrDefault();

            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            return Ok(supplier);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Save(SupplierSaveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Suppliers.Any(e => e.Id == dto.Id) == false)
            {
                var supplier = new Supplier
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    CreatedAt = dto.CreatedAt
                };

                _context.Suppliers.Add(supplier);
                _context.SaveChanges();

                return Ok(supplier);
            }

            var existingSupplier = _context.Suppliers.Find(dto.Id);

            if (existingSupplier == null)
            {
                return NotFound("Supplier not found");
            }

            existingSupplier.Name = dto.Name;
            existingSupplier.Email = dto.Email;
            existingSupplier.Phone = dto.Phone;
            existingSupplier.Address = dto.Address;
            existingSupplier.CreatedAt = dto.CreatedAt;

            _context.SaveChanges();

            return Ok(existingSupplier);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(e => e.Id == id);

            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            return Ok("Supplier deleted successfully");
        }
    }
}