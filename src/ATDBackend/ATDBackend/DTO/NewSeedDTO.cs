namespace ATDBackend.DTO
{
    public class NewSeedDTO
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int SchooolId { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public bool Is_active { get; set; }
        public byte[] Image { get; set; }
    }
}
