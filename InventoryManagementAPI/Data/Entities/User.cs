using System;
using System.Collections.Generic;

namespace InventoryManagementAPI.Data.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Usename { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
