using System.Threading.Tasks;

namespace Mearcury.Data;

public interface IMearcuryDbSchemaMigrator
{
    Task MigrateAsync();
}
