using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DAL_Compte_Bancaire_SQL_Server
{
    public class DALDbContextGeneration
    {
        private static DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private static PooledDbContextFactory<ApplicationDbContext> _pooledDbContextFactory;
        static DALDbContextGeneration()
        {
            IConfigurationRoot configuration =
                 new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
                    .AddJsonFile("appsettings.json", false)
                    .Build();
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("LocationConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                .EnableSensitiveDataLogging()
#endif
                .Options;
            _pooledDbContextFactory = new PooledDbContextFactory<ApplicationDbContext>(_dbContextOptions);
        }
        public static ApplicationDbContext ObtenirApplicationDBContext()
        {
            return _pooledDbContextFactory.CreateDbContext();
        }
    }
}
