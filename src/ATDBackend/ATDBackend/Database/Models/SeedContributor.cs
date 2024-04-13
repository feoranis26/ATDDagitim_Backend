using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    [PrimaryKey(nameof(SeedId), nameof(SchoolId))]
    public class SeedContributor
    {
        public int SeedId { get; set; }

        [ForeignKey(nameof(SeedId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Seed Seed { get; set; }



        public int SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public School School { get; set; }
    }
}
