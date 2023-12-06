using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.Models
{
    public class School_Seed
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public School School { get; set; }

        [ForeignKey("Id")]
        public User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [ForeignKey("Id")]
        public Category? Category { get; set; }

        public int? Price { get; set; }

        public string? Photo { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Source { get; set; }

        [ForeignKey("Id")]
        public Seed Seed { get; set; }

        [Required]
        public bool Is_deleted { get; set; }
    }
}
