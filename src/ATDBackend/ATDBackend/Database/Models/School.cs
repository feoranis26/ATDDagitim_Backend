using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public List<Order> Orders { get; set; } //FOREIGN KEY

        [Required]
        public float Credit { get; set; }


        public List<SeedContributor> SeedContributors { get; set; } //FOREIGN KEY


    }
}
