using System.ComponentModel.DataAnnotations;

namespace ATDBackend.Database.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
