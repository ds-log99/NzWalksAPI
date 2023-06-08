using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalksDpAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var reader = "91dd33d2-f75b-4eee-8f33-b1ac04328997";
            var writer = "3cd88ad9-2c6c-4e91-9958-f40fe55a894c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = reader,
                    ConcurrencyStamp = reader,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole()
                {
                    Id = writer,
                    ConcurrencyStamp =  writer,
                    Name = "Writter",
                    NormalizedName = "Writter".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
   
}
