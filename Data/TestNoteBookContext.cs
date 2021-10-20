using TestNotebook.Models;
using Microsoft.EntityFrameworkCore;

namespace TestNotebook.Data
{
    public class TestNoteBookContext : DbContext
    {
        public TestNoteBookContext(DbContextOptions<TestNoteBookContext> options) : base(options)
        {
        }

        public DbSet<Header> Header { get; set; }
        public DbSet<Detail> Detail { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Control> Control { get; set; }
        public DbSet<Screen> Screen { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<Result> Result { get; set; }
    }
}