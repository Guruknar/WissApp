using AppCore.Repository;
using AppCore.Repository.Base;
using AppCore.UnitOFwork;
using AppCore.UnitOFwork.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Service.Base
{
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : class,new()
    {
        private DbContext db;
        protected RepositoryBase<TEntity> repository;
        protected UnitOfWorkBase unitOfWork;

        public ServiceBase(DbContext _db)
        {
            db = _db;
            repository = new Repository<TEntity>(db);
            unitOfWork = new UnitOfWork(db);
        }

        #region Dispose,garbagecollector

        protected bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed && disposing)
            {
                if (unitOfWork != null)
                    unitOfWork.Dispose();
                if (repository != null)
                    repository.Dispose();
                if (db != null)
                    db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion



    }
}
