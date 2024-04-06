namespace ATDBackend.DTO
{
    public class OrderToModifyDTO
    {
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
