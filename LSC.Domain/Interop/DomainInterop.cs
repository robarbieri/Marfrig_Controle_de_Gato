using LSC.Domain.Interface;
using LSC.Infra.Data;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LSC.Domain.Interop
{

    public sealed class DomainInterop : IDisposable
    {

        #region Objetos/Variáveis locais

        private readonly LSCContext _ctx;

        #endregion

        #region Construtores

        public DomainInterop() : this(false) { }

        public DomainInterop(bool ensureTransactions)
        {
            _ctx = new LSCContext();
            _ctx.Configuration.EnsureTransactionsForFunctionsAndCommands = ensureTransactions;
        }

        #endregion

        #region Métodos de Serviços

        public TService CreateInstance<TService>()
            where TService : IServiceDomain
        {
            return (TService)Activator.CreateInstance(typeof(TService), _ctx);
        }

        #endregion

        #region Métodos Transação

        public DbContextTransaction BeginTransaction()
        {
            return _ctx.Database.BeginTransaction();
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _ctx.Database.BeginTransaction(isolationLevel);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _ctx.Database.ExecuteSqlCommand(sql, parameters);
        }

        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return _ctx.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
        }

        #endregion

        #region Métodos DbContext (Public)

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }

        #endregion

        #region Métodos DbContext (Internal)

        internal DbChangeTracker ChangeTracker { get { return _ctx.ChangeTracker; } }

        internal DbContextConfiguration Configuration { get { return _ctx.Configuration; } }

        internal Database Database { get { return _ctx.Database; } }

        internal DbEntityEntry Entry(object entity)
        {
            return _ctx.Entry(entity);
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _ctx.Entry<TEntity>(entity);
        }

        public DbSet Set(Type entityType)
        {
            return _ctx.Set(entityType);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _ctx.Set<TEntity>();
        }

        public DbContext Context()
        {
            return _ctx;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    _ctx.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        ~DomainInterop()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
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
