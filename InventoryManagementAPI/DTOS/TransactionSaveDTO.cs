namespace InventoryManagementAPI.DTOS
{
    public class TransactionSaveDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string TransactionType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Remarks { get; set; }
    }
}