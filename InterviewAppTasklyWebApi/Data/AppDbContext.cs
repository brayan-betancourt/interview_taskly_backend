using InterviewAppTasklyWebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InterviewAppTasklyWebApi.Entities;

namespace InterviewAppTasklyWebApi.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<TaskState> TaskStates { get; set; }
    public virtual DbSet<TaskPriority> TaskPriorities { get; set; }
    public virtual DbSet<TaskManagement> TaskManagements { get; set; }
    public virtual DbSet<TaskStateManagement> TaskStateManagements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FirstName)
                .HasMaxLength(30);

            entity.Property(u => u.LastName)
                .HasMaxLength(30);
        });
        
        modelBuilder.Entity<TaskState>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasIndex(s => s.Name).IsUnique();
            
            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(20);
        });
        
        modelBuilder.Entity<TaskPriority>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.Name).IsUnique();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);
        });
        
        modelBuilder.Entity<TaskManagement>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.HasIndex(c => c.Title);
            
            entity.HasIndex(c => c.CreationDate);
            
            entity.HasOne(m => m.Priorities)
                .WithMany(m => m.TaskManagements)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(m => m.States)
                .WithMany(m => m.TaskManagements)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(m => m.Users)
                .WithMany(m => m.TaskManagements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        });
        
        modelBuilder.Entity<TaskStateManagement>(entity =>
        {
            entity.HasKey(m => m.Id);
            
            entity.HasOne(m => m.TasksManagements)
                .WithMany(m => m.TaskStates)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(m => m.States)
                .WithMany(m => m.TaskStateManagements)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);

        });
    }
}