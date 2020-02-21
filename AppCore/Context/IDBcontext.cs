using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AppCore.Context
{
    public interface IDBcontext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        void Dispose();

    }
}
