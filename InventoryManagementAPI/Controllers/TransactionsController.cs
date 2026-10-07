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
    public class TransactionsController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet]
        [HttpGet]
        public IActionResult Get()
        {
            var transactions = _context.Transactions
                .Include(e => e.Product)
                .Include(e => e.User)
                .Select(e => new TransactionDTO
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    ProductName = e.Product.Name,
                    UserId = e.UserId,
                    Username = e.User.Usename,
                    TransactionType = e.TransactionType,
                    Quantity = e.Quantity,
                    TransactionDate = e.TransactionDate,
                    Remarks = e.Remarks
                })
                .ToList();

            return Ok(transactions);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var transaction = _context.Transactions
                .Include(e => e.Product)
                .Include(e => e.User)
                .Where(e => e.Id == id)
                .Select(e => new TransactionDTO
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    ProductName = e.Product.Name,
                    UserId = e.UserId,
                    Username = e.User.Usename,
                    TransactionType = e.TransactionType,
                    Quantity = e.Quantity,
                    TransactionDate = e.TransactionDate,
                    Remarks = e.Remarks
                })
                .FirstOrDefault();

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult Save(TransactionSaveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Transactions.Any(e => e.Id == dto.Id) == false)
            {
                var transaction = new Transaction
                {
                    ProductId = dto.ProductId,
                    UserId = dto.UserId,
                    TransactionType = dto.TransactionType,
                    Quantity = dto.Quantity,
                    TransactionDate = dto.TransactionDate,
                    Remarks = dto.Remarks
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                return Ok(transaction);
            }

            var existingTransaction = _context.Transactions.Find(dto.Id);

            if (existingTransaction == null)
            {
                return NotFound("Transaction not found");
            }

            existingTransaction.ProductId = dto.ProductId;
            existingTransaction.UserId = dto.UserId;
            existingTransaction.TransactionType = dto.TransactionType;
            existingTransaction.Quantity = dto.Quantity;
            existingTransaction.TransactionDate = dto.TransactionDate;
            existingTransaction.Remarks = dto.Remarks;

            _context.SaveChanges();

            return Ok(existingTransaction);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(e => e.Id == id);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return Ok("Transaction deleted successfully");
        }
    }
}