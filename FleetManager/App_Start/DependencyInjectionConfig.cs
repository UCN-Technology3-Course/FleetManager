using FleetManager.DataAccessLayer;
using FleetManager.DataAccessLayer.Model;
using System.Web.Http;
using Unity;

namespace FleetManager.App_Start
{
    public static class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var container = new UnityContainer();
            container.RegisterFactory<IRepository<Car>>(c => RepositoryFactory.Create<Car>());
            container.RegisterFactory<IRepository<Location>>(c => RepositoryFactory.Create<Location>());
            configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}