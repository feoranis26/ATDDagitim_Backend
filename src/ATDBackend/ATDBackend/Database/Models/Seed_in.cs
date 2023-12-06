using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    public class Seed_in
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id")]
        public School School { get; set; }

        [Required]
        [ForeignKey("Id")]
        public User User { get; set; }

        [Required]
        [ForeignKey("Id")]
        public School_Seed School_Seed { get; set; }

        [Required]
        public String Seed_name { get; set; }

        public String? Seed_description { get; set; }

        [Required]
        public int Seed_amount { get; set; }

        [Required]
        public int Status { get; set; }

        [ForeignKey("Id")]
        public Category Category_id { get; set; }

        public string? Notes { get; set; }

        public string? Cargo_Tracking_Number { get; set; }
    }
}
