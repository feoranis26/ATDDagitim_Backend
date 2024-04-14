using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATDBackend.Database.Models
{
    [Table("BasketSeeds")]
    [PrimaryKey(nameof(SeedId), nameof(UserId))]
    public class BasketSeed
    {
        public int SeedId { get; set; }

        [ForeignKey(nameof(SeedId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Seed Seed { get; set; }



        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; }


        public int Quantity { get; set; }
    }
}
