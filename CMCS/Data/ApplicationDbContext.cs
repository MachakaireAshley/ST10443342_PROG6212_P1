using Microsoft.EntityFrameworkCore;
using CMCS.Models;
 
namespace CMCS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationships
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.User)
                .WithMany(u => u.Claims)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Claim)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            //Initial data showing users and roles for demonstration
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Joice",
                    LastName = "Muzangwa",
                    Email = "jMuz@gmail.com",
                    Password = "hashed_password", // This would be properly hashed in an actual application
                    Role = UserRole.Lecturer,
                    DateRegistered = DateTime.Now.AddMonths(-3)
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "janeSmith@gmail.com",
                    Password = "hashed_password",
                    Role = UserRole.ProgramCoordinator,
                    DateRegistered = DateTime.Now.AddMonths(-2)
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Ashley",
                    LastName = "Machakaire",
                    Email = "MacAsh@example.com",
                    Password = "hashed_password",
                    Role = UserRole.AcademicManager,
                    DateRegistered = DateTime.Now.AddMonths(-1)
                }
            );
        }
    }
}
}