using Microsoft.EntityFrameworkCore;
using ExamResultManagement.Core.Models;

namespace ExamResultManagement.Core.Data
{
    public class ExamResultContext : DbContext
    {
        public ExamResultContext(DbContextOptions<ExamResultContext> options)
            : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
    }
}
