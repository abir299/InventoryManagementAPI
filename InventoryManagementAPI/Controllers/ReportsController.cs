using InventoryManagementAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class ReportsController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpGet("Summary")]
        public IActionResult Summary()
        {
            var totalProducts = _context.Products.Count();
            var totalCategories = _context.Categories.Count();
            var totalSuppliers = _context.Suppliers.Count();
            var totalStock = _context.Stocks.Sum(e => e.Quantity);

            var totalPurchases = _context.Transactions
                .Where(e => e.TransactionType == "Purchase")
                .Sum(e => e.Quantity);

            var totalSales = _context.Transactions
                .Where(e => e.TransactionType == "Sale")
                .Sum(e => e.Quantity);

            return Ok(new
            {
                totalProducts,
                totalCategories,
                totalSuppliers,
                totalStock,
                totalPurchases,
                totalSales
            });
        }

        [HttpGet("LowStock")]
        public IActionResult LowStock(int threshold = 5)
        {
            var products = _context.Stocks
                .Include(e => e.Product)
                .Where(e => e.Quantity <= threshold)
                .Select(e => new
                {
                    e.ProductId,
                    ProductName = e.Product.Name,
                    e.Quantity
                })
                .ToList();

            return Ok(products);
        }
    }
}