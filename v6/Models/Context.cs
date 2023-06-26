using Microsoft.EntityFrameworkCore;

namespace v6.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentModel> Students { get; set; }
    }
}
