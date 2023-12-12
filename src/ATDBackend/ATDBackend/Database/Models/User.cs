using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ATDBackend.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone_number { get; set; }

        [Required]
        public string Hashed_PW { get; set; }

        public int SchoolId { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey("School_Id")]
        public School School_id { get; set; } //FOREIGN KEY

        public int RoleId { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey("Role_Id")]
        public Role Role_id { get; set; } //FOREIGN KEY

        [Required]
        public DateTime Register_date { get; set; }

        public string Username { get; set; }
    }
}
