using dotnetcore.api.entity.Users;
using dotnetcore.api.template.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore.api.template.Data
{
    public class ApiContext : DbContext, IDbContext
    {
        #region "Constructors"

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public ApiContext()
        {
        }

        #endregion

        #region "Entities"

        public DbSet<UserEntity> Users { get; set; }

        #endregion

        #region "Public Methods"

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        #endregion
    }
}
