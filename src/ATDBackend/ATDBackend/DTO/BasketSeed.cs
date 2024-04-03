namespace ATDBackend.DTO
{
    public class BasketSeed
    {
        public int Id { get; set; } //Id of the seed (non-changable)
        public required string Name { get; set; } //name of the seed
        public int CategoryId { get; set; } //category ID when the item was purchased
        public int Stock { get; set; } //stock when the item was purchased
        public float Price { get; set; } //price when the item was purchased
        public int? Quantity { get; set; } //quantity of the seed added to basket
    }
}
