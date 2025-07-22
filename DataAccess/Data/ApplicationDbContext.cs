using Microsoft.EntityFrameworkCore;
using Models;
using Utility;


namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet declarations
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetail> TicketDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Concession> Concessions { get; set; }
        public DbSet<ConcessionDetail> ConcessionDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set delete behavior to Restrict for all FK to prevent cascade deletes
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Optional: Sample Fluent Configs (extend if needed)
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Concession>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Price).HasColumnType("decimal(18,0)");
            });

            modelBuilder.Entity<ConcessionDetail>(entity =>
            {
                entity.Property(cd => cd.UnitPrice).HasColumnType("decimal(18,0)");
                entity.HasOne(cd => cd.Concession)
                      .WithMany(c => c.ConcessionDetails)
                      .HasForeignKey(cd => cd.ConcessionId);

                entity.HasOne(cd => cd.Order)
                      .WithMany(o => o.ConcessionDetails)
                      .HasForeignKey(cd => cd.OrderId);
            });

            modelBuilder.Entity<TicketDetail>(entity =>
            {
                entity.Property(td => td.UnitPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(t => t.TotalPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.Property(t => t.Price).HasColumnType("decimal(18, 0)");
            });

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@dtv.com",
                    PasswordHash = "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW", // "123123"
                    Name = "Admin",
                    Role = Constant.UserRole_Admin,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Email = "manager@dtv.com",
                    PasswordHash = "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW", // "123123"
                    Name = "Manager",
                    Role = Constant.UserRole_Manager,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Id = 3,
                    Email = "staff@dtv.com",
                    PasswordHash = "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW", // "123123"
                    Name = "Staff",
                    Role = Constant.UserRole_Staff,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Id = 4,
                    Email = "user@dtv.com",
                    PasswordHash = "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW", // "123123"
                    Name = "Customer",
                    Role = Constant.UserRole_Customer,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );

        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        //This method sets the CreatedAt and UpdatedAt fields for entities
        private void SetAuditFields()
        {
            var now = TimeHelper.GetVietnamTime();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "CreatedAt" && p.CurrentValue == null))
                    {
                        entry.Property("CreatedAt").CurrentValue = now;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "UpdatedAt"))
                    {
                        entry.Property("UpdatedAt").CurrentValue = now;
                    }
                }
            }
        }
    }
}
