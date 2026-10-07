namespace InventoryManagementAPI.DTOS
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string TransactionType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Remarks { get; set; }
    }
}