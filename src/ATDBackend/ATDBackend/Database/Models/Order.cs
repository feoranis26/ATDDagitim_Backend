using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id")]
        public School School { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey("Id")]
        public User User { get; set; } //FOREIGN KEY

        [Required]
        public ICollection<Seed> Seeds { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
