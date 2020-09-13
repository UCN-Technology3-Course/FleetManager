using FleetManager.DataAccessLayer.Model;
using FleetManager.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer
{
    public static class RepositoryFactory
    {
        public static IRepository<TEntity> Create<TEntity>()
        {
            Type modelType = typeof(TEntity);

            switch (modelType.Name)
            {
                case "Car":
                    return new CarsRepository() as IRepository<TEntity>;
                case "Location":
                    return new LocationRepository() as IRepository<TEntity>;
                default:
                    throw new RepositoryException(string.Format("Unknown repository: {0}", modelType.Name));
            }


        }
    }
}
