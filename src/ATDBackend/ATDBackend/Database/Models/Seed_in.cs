using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    public class Seed_in
    {
        [Key]
        public int Id { get; set; }

        public int SchoolId { get; set; } // Foreign key property for School

        [Required]
        [ForeignKey("SchoolId")]
        public School School { get; set; }

        public int UserId { get; set; } // Foreign key property for User

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int SchoolSeedId { get; set; } // Foreign key property for School_Seed

        [Required]
        [ForeignKey("SchoolSeedId")]
        public School_Seed School_Seed { get; set; }

        [Required]
        public String Seed_name { get; set; }

        public String? Seed_description { get; set; }

        [Required]
        public int Seed_amount { get; set; }

        [Required]
        public int Status { get; set; }

        public int? CategoryId { get; set; } // Foreign key property for Category

        [ForeignKey("CategoryId")]
        public Category? Category_id { get; set; }

        public string? Notes { get; set; }

        public string? Cargo_Tracking_Number { get; set; }
    }
}
