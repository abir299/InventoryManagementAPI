namespace InventoryManagementAPI.DTOS
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductCount { get; set; }
    }
}