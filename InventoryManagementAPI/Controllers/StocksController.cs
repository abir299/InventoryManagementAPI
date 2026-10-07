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
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class StocksController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var stocks = _context.Stocks
                .Include(e => e.Product)
                .Select(e => new StockDTO
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    ProductName = e.Product.Name,
                    Quantity = e.Quantity,
                    UpdatedAt = e.UpdatedAt
                })
                .ToList();

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var stock = _context.Stocks
                .Include(e => e.Product)
                .Where(e => e.Id == id)
                .Select(e => new StockDTO
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    ProductName = e.Product.Name,
                    Quantity = e.Quantity,
                    UpdatedAt = e.UpdatedAt
                })
                .FirstOrDefault();

            if (stock == null)
            {
                return NotFound("Stock not found");
            }

            return Ok(stock);
        }

        [HttpPost]
        public IActionResult Save(StockSaveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Stocks.Any(e => e.Id == dto.Id) == false)
            {
                var stock = new Stock
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UpdatedAt = dto.UpdatedAt
                };

                _context.Stocks.Add(stock);
                _context.SaveChanges();

                return Ok(stock);
            }

            var existingStock = _context.Stocks.Find(dto.Id);

            if (existingStock == null)
            {
                return NotFound("Stock not found");
            }

            existingStock.ProductId = dto.ProductId;
            existingStock.Quantity = dto.Quantity;
            existingStock.UpdatedAt = dto.UpdatedAt;

            _context.SaveChanges();

            return Ok(existingStock);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(e => e.Id == id);

            if (stock == null)
            {
                return NotFound("Stock not found");
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return Ok("Stock deleted successfully");
        }
    }
}