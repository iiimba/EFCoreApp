using Microsoft.EntityFrameworkCore;

namespace ExistingDb.Models.Manual
{
    public class ManualContext : DbContext
    {
        public ManualContext(DbContextOptions<ManualContext> options)
            : base(options)
        {

        }

        public DbSet<Shoe> Shoes { get; set; }

        public DbSet<Style> ShoeStyles { get; set; }
    }
}
