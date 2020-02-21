using AppCore.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : class, new()
    {
        #region Dispose,garbagecollector
        protected bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected IDBcontext db;
        //protected DbContext db;
        public RepositoryBase(IDBcontext _db)
        {
            db = _db;
        }

        public virtual List<TEntity> GetEntities()
        {
            try
            {
                return db.Set<TEntity>().ToList();
            }
            catch (Exception exc)
            {

                throw exc;
            }
            
        }



    }
}
