using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Framework.Configuration;

namespace TheWorld.Models
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        private IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            _config = config;
        }

        //Esta propiedades permiten hacer consultas sobre la bd usando LinQ
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
