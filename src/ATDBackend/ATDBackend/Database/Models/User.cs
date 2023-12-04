using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ATDBackend.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string HashedPW { get; set; }
    }
}
