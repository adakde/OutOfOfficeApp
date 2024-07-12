using Microsoft.EntityFrameworkCore;
namespace OutOfOfficeApp.Data
{
    public class OutOfOfficeAppDbContext : DbContext
    {
        public OutOfOfficeAppDbContext(DbContextOptions<OutOfOfficeAppDbContext> options)
               : base(options)
        { }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
    }
}