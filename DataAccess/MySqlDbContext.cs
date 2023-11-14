using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MySqlDbContext : IdentityDbContext
    {

        #region DbSet
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Usermeta>? Usermeta { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        #endregion


        public MySqlDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usermeta>(opts =>
            {
                opts
                    .HasOne(m => m.User)
                    .WithOne(u => u.Usermeta)
                    .HasForeignKey<Usermeta>(m => m.UserId);
            });

            builder.Entity<User>(opts =>
            {
                opts
                    .HasOne(u => u.Usermeta)
                    .WithOne(u => u.User)
                    .HasForeignKey<User>(u => u.MetaId);
            });

            builder.Entity<Department>(opts =>
            {
                opts
                    .HasMany(d => d.Members)
                    .WithOne(e => e.Department);
            });

            builder.Entity<Employee>(opts =>
            {
                opts
                    .HasOne(e => e.Supervisor)
                    .WithMany(e => e.TeamMembers)
                    .HasForeignKey(e => e.SupervisorId);
                opts
                    .HasOne(e => e.Department)
                    .WithMany(d => d.Members)
                    .HasForeignKey(e => e.DepartmentId);
            });

            base.OnModelCreating(builder);
            Seeding(builder);
        }

        private static void Seeding(ModelBuilder builder)
        {
            SeedRole(builder);
        }

        private static void SeedRole(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Name = "Employee", NormalizedName = "EMPLOYEE" },
                new Role { Name = "Client", NormalizedName = "CLIENT" }
            );
        }
    }
}
