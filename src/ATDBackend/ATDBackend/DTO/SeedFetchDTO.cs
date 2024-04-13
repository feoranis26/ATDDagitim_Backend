using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATDBackend.DTO
{
    public class SeedFetchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public string CategoryName { get; set; }

        public string[] ContributorSchoolNames { get; set; }

        public int Stock { get; set; }

        public float Price { get; set; }

        public bool Is_active { get; set; }

    }
}
