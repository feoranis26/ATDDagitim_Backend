using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATDBackend.DTO;
using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.Models
{
    [Index(nameof(Email), nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Phone_number { get; set; }

        [Required]
        public string Hashed_PW { get; set; }

        public List<BasketSeed>? BasketSeeds { get; set; }

        [Required]
        public DateTime Register_date { get; set; }


        public int SchoolId { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey(nameof(SchoolId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public School School { get; set; } //FOREIGN KEY


        public int RoleId { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey(nameof(RoleId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Role Role { get; set; } //FOREIGN KEY
    }
}
