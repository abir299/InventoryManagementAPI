using System;
using System.Collections.Generic;

namespace InventoryManagementAPI.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
