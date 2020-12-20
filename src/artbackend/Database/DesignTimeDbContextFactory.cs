using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace artbackend.Database
{

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LikesContext>
    {
        private readonly IConfiguration configuration;

        public DesignTimeDbContextFactory()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public LikesContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LikesContext>();
            builder.UseSqlServer(configuration.GetSection("database")["connectionString"]);
            return new LikesContext(builder.Options);
        }
    }
}