using LSC.Cross.Interface;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace LSC.Infra.Repository.Interface
{
    public interface ILSCRepository<TTransport, TTypeKey>
        where TTransport : ILSCTransport
        where TTypeKey : struct
    {

        #region Propriedades

        void Add(TTransport transport);

        ICollection<TTransport> GetAll();

        TTransport GetByKey(TTypeKey key);

        void Remove(TTransport transport);

        void Update(TTransport transport);

        //DbSet<TEntity> Set();

        //LSCContext Context();

        #endregion

    }
}
