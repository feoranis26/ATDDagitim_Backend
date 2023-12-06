using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ATDBackend.Database.Models
{
    public class Seed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Name { get; set; }

        [Required]
        [ForeignKey("Category_id")]
        public Category Category { get; set; } //FOREIGN KEY

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Id")]
        public User User_id { get; set; } //FOREIGN KEY

        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime Date_added { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool Is_active { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
