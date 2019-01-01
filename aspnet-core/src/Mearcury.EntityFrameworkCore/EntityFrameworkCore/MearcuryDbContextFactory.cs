using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mearcury.Configuration;
using Mearcury.Web;

namespace Mearcury.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MearcuryDbContextFactory : IDesignTimeDbContextFactory<MearcuryDbContext>
    {
        public MearcuryDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MearcuryDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MearcuryDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MearcuryConsts.ConnectionStringName));

            return new MearcuryDbContext(builder.Options);
        }
    }
}
