using System.ComponentModel.DataAnnotations;

namespace ATDBackend.Database.Models
{
    public class School 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Credit { get; set; }
    }
}