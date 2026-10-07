using System;
using System.Collections.Generic;

namespace InventoryManagementAPI.Data.Entities;

public partial class Stock
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product? Product { get; set; }
}
