using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATDBackend.DTO;
using ATDBackend.Database.Models;

namespace ATDBackend.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

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
        public Role Role { get; set; } //FOREIGN KEY

        public ICollection<BasketSeed> Basket { get; set; }

        [Required]
        public DateTime Register_date { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
