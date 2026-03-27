using Microsoft.EntityFrameworkCore;
using illiminatado_exam.Model;
namespace illiminatado_exam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PayReg> PayRegs { get; set; }
        public DbSet<OtReg> OtReg { get; set; }
        public DbSet<TimeAttendance> TimeAttendances { get; set; }
    }
}
