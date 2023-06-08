using Microsoft.EntityFrameworkCore;
using NZwalksDpAPI.Models.Domain;

namespace NZwalksDpAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) :base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties 
            // easy, medium, hard 

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                      Id = Guid.Parse("48083071-c62b-4357-986c-b99d230216b1"),
                      Name = "Easy"

                },
                   new Difficulty()
                {
                      Id = Guid.Parse("015cdd08-18a5-40d2-bfa9-914e111bc983"),
                      Name = "Medium"

                },
                      new Difficulty()
                {
                      Id = Guid.Parse("86ee1913-90cd-4bc7-ab82-35c18e70fe54"),
                      Name = "Hard"

                },

            };

            // Seed difficulties to the database 
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("df866e58-cc4f-4f65-8d85-d354be56f6aa"),
                    Name = "Oakland",
                    Code = "OAK",
                    RegionImageUrl = "Some-oakland-image-url"
                },
                    new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions); 
        }


    }
}
