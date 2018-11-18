namespace ContosoUniversity.Models
{
    using Microsoft.EntityFrameworkCore;

    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<Course> Course { get; set; }
    }
}