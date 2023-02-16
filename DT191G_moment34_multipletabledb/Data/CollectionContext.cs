using DT191G_moment34_multipletabledb.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_moment34_multipletabledb.Data
{
    public class CollectionContext : DbContext
    {
        // constructor
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {
        }

        public DbSet<Collection> Collection { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Borrowed> Borrowed { get; set; }

    }
}
