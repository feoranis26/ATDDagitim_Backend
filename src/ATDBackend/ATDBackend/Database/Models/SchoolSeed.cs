using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.Models
{
    public class SchoolSeed
    {
        [Key]
        public int Id { get; set; }


        public int SchoolId { get; set; } 

        [Required]
        [ForeignKey(nameof(SchoolId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public School School { get; set; }


        public int UserId { get; set; } // Foreign key property for User

        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; }


        public int CategoryId { get; set; } // Foreign key property for Category

        [ForeignKey(nameof(CategoryId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Category? Category { get; set; }


        public int SeedId { get; set; } // Foreign key property for Seed

        [ForeignKey(nameof(SeedId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Seed Seed { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        public int? Price { get; set; }

        public string? Photo { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Source { get; set; }

        [Required]
        public bool Is_deleted { get; set; }
    }
}