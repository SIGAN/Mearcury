using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mearcury.Data;
using Volo.Abp.DependencyInjection;

namespace Mearcury.EntityFrameworkCore;

public class EntityFrameworkCoreMearcuryDbSchemaMigrator
    : IMearcuryDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMearcuryDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MearcuryDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MearcuryDbContext>()
            .Database
            .MigrateAsync();
    }
}
