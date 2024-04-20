using BookCrudApi.Books.Model;
using Microsoft.EntityFrameworkCore;

namespace BookCrudApi.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }

    }
}
