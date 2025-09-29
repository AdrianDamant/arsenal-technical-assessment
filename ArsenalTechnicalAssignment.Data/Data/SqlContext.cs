using ArsenalTechnicalAssignment.Data.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ArsenalTechnicalAssignment.Data.Data
{
    public partial class SqlContext : DbContext
    {
        private readonly string _connectionString;

        public SqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("ArsenalConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<FootballClub> FootballClubs { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}

