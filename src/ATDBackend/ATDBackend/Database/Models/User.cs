using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone_number { get; set; }

        [Required]
        public string Hashed_PW { get; set; }

        [Required]
        public int School_id { get; set; } //FOREIGN KEY

        [Required]
        public int Role_id { get; set; } //FOREIGN KEY

        [Required]
        public DateTime Register_date { get; set; }

        public string Username { get; set; }
    }
}
