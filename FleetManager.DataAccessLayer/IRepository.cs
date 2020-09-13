using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        int Insert(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(int id);
    }
}
