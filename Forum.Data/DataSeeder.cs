using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public static class DataSeeder
    {
        public static void SeedUsers(this ModelBuilder modelBuilder) 
        {
            PasswordHasher<IdentityUser> hasher = new();
        }
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole { Id = "5CA780EC-F0F6-4FF1-B6A6-A64AD279B478", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "1B179F4D-BA09-4EDC-BE27-229042A6DB98", Name = "Member", NormalizedName = "MEMBER" }
                );
        }

    }
}
