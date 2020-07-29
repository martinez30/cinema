using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class CineContextFactory : DesignTimeDBContextFactoryBase<CineContext>
    {
        protected override CineContext CreateNewInstance(DbContextOptions<CineContext> options)
        {
            return new CineContext(options);
        }
    }
}
