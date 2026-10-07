using System;
using System.Collections.Generic;

namespace InventoryManagementAPI.Data.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Remarks { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
