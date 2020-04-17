using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcSunCity.Data
{
    public class MvcSunCityContext : IdentityDbContext
    {
        public MvcSunCityContext (DbContextOptions<MvcSunCityContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
    }
}