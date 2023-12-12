using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int SchoolId { get; set; } // Foreign key property for School

        [Required]
        [ForeignKey("SchoolId")]
        public School School { get; set; } //FOREIGN KEY

        public int UserId { get; set; } // Foreign key property for User

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; } //FOREIGN KEY

        [Required]
        public List<int> Seeds { get; set; }

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
