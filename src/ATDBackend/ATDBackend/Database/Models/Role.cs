using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ATDBackend.Database.Models{

    [Index(nameof(RoleName), IsUnique = true)]
    public class Role{
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        public ulong Permissions { get; set; }
    }
}