using Microsoft.EntityFrameworkCore;
using MvcSunCity.Models;

namespace MvcSunCity.Data
{
    public class MvcSunCityContext : DbContext
    {
        public MvcSunCityContext (DbContextOptions<MvcSunCityContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
    }
}