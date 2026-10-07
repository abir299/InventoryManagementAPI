namespace InventoryManagementAPI.DTOS
{
    public class ProductSaveDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}