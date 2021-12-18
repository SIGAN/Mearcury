using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Mearcury.EntityFrameworkCore
{
    public static class MearcuryDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MearcuryDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MearcuryDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
