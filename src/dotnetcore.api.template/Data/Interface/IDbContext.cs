using Microsoft.EntityFrameworkCore;

namespace dotnetcore.api.template.Data.Interface
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
