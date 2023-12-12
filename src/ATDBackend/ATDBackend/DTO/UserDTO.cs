namespace ATDBackend.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Hashed_PW { get; set; }
        public int SchoolId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
    }
}
