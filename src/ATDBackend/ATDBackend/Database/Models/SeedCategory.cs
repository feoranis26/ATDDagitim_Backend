using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    public class SeedCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category_name { get; set; }
    }
}
