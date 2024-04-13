using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ATDBackend.Database.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Seed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public string Description { get; set; }


        public int CategoryId { get; set; } //FOREIGN KEY

        [Required]
        [ForeignKey(nameof(CategoryId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Category Category { get; set; } //FOREIGN KEY



        public List<SeedContributor> SeedContributors { get; set; } //FOREIGN KEY


        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime Date_added { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public bool Is_active { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
