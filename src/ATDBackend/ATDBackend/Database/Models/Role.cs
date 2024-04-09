using System.ComponentModel.DataAnnotations;

namespace ATDBackend.Database.Models{

    public class Role{
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Role_name { get; set; }
    }
}