using Microsoft.EntityFrameworkCore;
namespace OutOfOfficeApp.Data
{
    public class OutOfOfficeAppDbContext : DbContext
    {
        public OutOfOfficeAppDbContext(DbContextOptions<OutOfOfficeAppDbContext> options)
               : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.Subdivision).IsRequired();
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.OutOfOfficeBalance).IsRequired();
                entity.Property(e => e.Photo);

                entity
                    .HasOne(e => e.PeoplePartner)
                    .WithMany()
                    .HasForeignKey(e => e.PeoplePartnerId)
                    .OnDelete(DeleteBehavior.NoAction);  // Prevent cascade delete to avoid cycles
            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasKey(lr => lr.ID);
                entity.Property(lr => lr.StartDate).IsRequired();
                entity.Property(lr => lr.EndDate).IsRequired();
                entity.Property(lr => lr.Status).IsRequired();

                entity
                    .HasOne(lr => lr.Employee)
                    .WithMany()
                    .HasForeignKey(lr => lr.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);  // Prevent cascade delete to avoid cycles
            });

            modelBuilder.Entity<ApprovalRequest>(entity =>
            {
                entity.HasKey(ar => ar.ID);
                entity.Property(ar => ar.Status).IsRequired();
                entity.Property(ar => ar.Comment).IsRequired();

                entity
                    .HasOne(ar => ar.Approver)
                    .WithMany()
                    .HasForeignKey(ar => ar.ApproverId)
                    .OnDelete(DeleteBehavior.NoAction);  // Prevent cascade delete to avoid cycles

                entity
                    .HasOne(ar => ar.LeaveRequest)
                    .WithMany()
                    .HasForeignKey(ar => ar.LeaveRequestId)
                    .OnDelete(DeleteBehavior.NoAction);  // Prevent cascade delete to avoid cycles
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.ProjectType).IsRequired();
                entity.Property(p => p.StartDate).IsRequired();
                entity.Property(p => p.Status).IsRequired();

                entity
                    .HasOne(p => p.ProjectManager)
                    .WithMany()
                    .HasForeignKey(p => p.ProjectManagerId)
                    .OnDelete(DeleteBehavior.NoAction);  // Prevent cascade delete to avoid cycles
            });

        }
    }
}
