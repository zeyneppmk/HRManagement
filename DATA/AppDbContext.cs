using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HRManagement.Entitites;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tablolar (DbSet'ler)
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<AppNotification> AppNotifications { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Pozisyon> Pozisyons { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. User ↔ UserDetail → One-to-One (gerekli)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserDetail)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDetail>(ud => ud.UserId);

            // 2. User ↔ Pozisyon → One-to-Many
            modelBuilder.Entity<Pozisyon>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pozisyons)
                .HasForeignKey(p => p.UserId);

            // 3. TaskItem ↔ AssignedTo (User) → One-to-Many
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedTo)
                .WithMany()
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict); // aynı tabloya iki FK olduğu için Restrict

            // 4. TaskItem ↔ AssignedBy (User) → One-to-Many
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedBy)
                .WithMany()
                .HasForeignKey(t => t.AssignedById)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Notification ↔ TaskItem → Optional One-to-One
            modelBuilder.Entity<AppNotification>()
                .HasOne(n => n.TaskItem)
                .WithMany()
                .HasForeignKey(n => n.TaskItemId)
                .OnDelete(DeleteBehavior.SetNull);

            // 6. Notification ↔ LeaveRequest → Optional One-to-One
            modelBuilder.Entity<AppNotification>()
                .HasOne(n => n.LeaveRequest)
                .WithMany()
                .HasForeignKey(n => n.LeaveRequestId)
                .OnDelete(DeleteBehavior.SetNull);

            // 7. Notification ↔ Sender (User)
            modelBuilder.Entity<AppNotification>()
                .HasOne(n => n.Sender)
                .WithMany()
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // 8. Notification ↔ Receiver (User)
            modelBuilder.Entity<AppNotification>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // 9. Payment ↔ User
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            // 10. LeaveRequest ↔ User
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<UserDetail>()
                .Property(u => u.Salary)
                .HasPrecision(18, 2);

        }
    }

}
