using GR30323_Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GR30323_Web.UI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
