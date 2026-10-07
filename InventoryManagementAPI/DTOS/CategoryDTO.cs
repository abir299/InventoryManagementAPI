namespace InventoryManagementAPI.DTOS
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductCount { get; set; }
    }
}