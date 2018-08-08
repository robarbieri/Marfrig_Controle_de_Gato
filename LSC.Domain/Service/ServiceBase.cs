using LSC.Cross.Interface;
using LSC.Cross.Transport;
using LSC.Domain.Interface;
using LSC.Infra.Data;
using LSC.Infra.Repository.Entities;
using LSC.Infra.Repository.Interface;
using System;
using System.Collections.Generic;

namespace LSC.Domain.Service
{
    public abstract class ServiceBase<TRepository, TTransport, TTypeKey> : IDisposable, IServiceDomain
        where TRepository : ILSCRepository<TTransport, TTypeKey>
        where TTransport : ILSCTransport
        where TTypeKey : struct
    {

        #region Objetos/Variáveis Locais

        protected readonly TRepository _repository;

        #endregion

        #region Construtores

        public ServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Métodos

        public virtual void Add(TTransport transport)
        {
            _repository.Add(transport);
        }

        public virtual void Update(TTransport transport)
        {
            _repository.Update(transport);
        }

        public virtual void Remove(TTransport transport)
        {
            _repository.Remove(transport);
        }

        public virtual IEnumerable<TTransport> GetAll()
        {
            return _repository.GetAll();

        }

        public virtual TTransport GetByKey(TTypeKey id)
        {
            return _repository.GetByKey(id);
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
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
