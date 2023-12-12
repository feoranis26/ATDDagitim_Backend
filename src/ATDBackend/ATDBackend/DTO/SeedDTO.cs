namespace ATDBackend.DTO
{
    public class SeedDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public bool Is_active { get; set; }
        public string Image { get; set; }
    }
}
