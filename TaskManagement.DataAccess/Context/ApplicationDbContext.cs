using Microsoft.EntityFrameworkCore;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.TaskStatus> TasksStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Entities.Task>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Entities.TaskStatus>(entity =>
            {
                entity.ToTable("TasksStatus");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
