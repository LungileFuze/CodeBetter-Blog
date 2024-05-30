using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBetter.API.Data
{
    public class AuthDBContext : IdentityDbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "d0ad614e-2813-4c54-9b84-a75312cfcf9e";
            var writerRoleId = "bc85e18c-5116-41f3-be07-9850f882e62a";

            //Create Reader and Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id= writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp= writerRoleId
                }
            };
            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create an Admin User
            var adminUserId = "1403872f-a209-48c8-b523-fa09d45c8582";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codebetter.com",
                Email = "admin@codebetter.com",
                NormalizedEmail = "admin@codebetter.com".ToUpper(),
                NormalizedUserName = "admin@codebetter.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }


    }
}
