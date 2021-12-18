using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Mearcury.Authorization.Roles;
using Mearcury.Authorization.Users;
using Mearcury.MultiTenancy;

namespace Mearcury.EntityFrameworkCore
{
    public class MearcuryDbContext : AbpZeroDbContext<Tenant, Role, User, MearcuryDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MearcuryDbContext(DbContextOptions<MearcuryDbContext> options)
            : base(options)
        {
        }
    }
}
