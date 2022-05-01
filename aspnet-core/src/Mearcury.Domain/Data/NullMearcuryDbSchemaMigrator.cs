using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Mearcury.Data;

/* This is used if database provider does't define
 * IMearcuryDbSchemaMigrator implementation.
 */
public class NullMearcuryDbSchemaMigrator : IMearcuryDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
