using LSC.Cross.Interface;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;
using LSC.Infra.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LSC.Infra.Repository.Entities
{
    public abstract class RepositoryBase<TEntity, TTransport, TTypeKey> : ILSCRepository<TTransport, TTypeKey>
        where TEntity : class, ILSCEntity
        where TTransport : class, ILSCTransport
        where TTypeKey : struct
    {

        #region Variáveis/Objetos Locais

        protected readonly LSCContext _ctx;

        #endregion

        #region Construtores/Destrutores

        public RepositoryBase(LSCContext ctx)
        {
            _ctx = ctx;
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }

        #endregion

        #region Métodos abstratos
        
        protected abstract TEntity TransportToEntity(TTransport transport);
        
        protected abstract TTransport EntityToTransport(TEntity entity);

        protected abstract TEntity FindByKey(TTransport transport);

        protected abstract void UpdateFieldByTransport(TEntity entity, TTransport transport);

        protected abstract void CatchId(TEntity entity, TTransport transport);

        #endregion

        #region Métodos Locais

        protected ICollection<TEntity> TransportToEntity(ICollection<TTransport> transport)
        {
            ICollection<TEntity> result = new List<TEntity>();
            foreach (TTransport item in transport)
            {
                result.Add(TransportToEntity(item));
            }
            return result;
        }

        protected ICollection<TTransport> EntityToTransport(ICollection<TEntity> transport)
        {
            ICollection<TTransport> result = new List<TTransport>();
            foreach (TEntity item in transport)
            {
                result.Add(EntityToTransport(item));
            }
            return result;
        }

        #endregion

        #region Métodos Públicos

        public void Add(TTransport transport)
        {
            TEntity entity = TransportToEntity(transport);
            _ctx.Set<TEntity>().Add(entity);
            _ctx.SaveChanges();
            CatchId(entity, transport);
        }

        public virtual ICollection<TTransport> GetAll()
        {
            return EntityToTransport(_ctx.Set<TEntity>().ToList());
        }

        public virtual TTransport GetByKey(TTypeKey key)
        {
            return EntityToTransport(_ctx.Set<TEntity>().Find(key));
        }

        public void Remove(TTransport transport)
        {
            TEntity entity = FindByKey(transport);
            _ctx.Set<TEntity>().Remove(entity);
            _ctx.SaveChanges();
        }

        public void Update(TTransport transport)
        {

            TEntity entity = FindByKey(transport);
            UpdateFieldByTransport(entity, transport);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public DbSet<TEntity> Set()
        {
            return _ctx.Set<TEntity>();
        }

        public LSCContext Context()
        {
            return _ctx;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
