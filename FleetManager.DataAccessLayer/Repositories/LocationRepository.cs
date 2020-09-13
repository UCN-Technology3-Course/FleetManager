using FleetManager.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Repositories
{
    class LocationRepository : IRepository<Location>
    {
        private static List<Location> data = new List<Location>
        {
            new Location{ Id = 1, Address="Killmotor Hill", City="Duckburg", Phone="555-1234", Zip="QCK555"},
            new Location{ Id = 2, Address="1313 Webfoot Street", City="Duckburg", Phone="555-4321", Zip="QCK555"}
        };

        public IEnumerable<Location> GetAll()
        {
            return data;
        }

        public Location GetById(int id)
        {
            return data.SingleOrDefault(l => l.Id == id);
        }

        public int Insert(Location entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Location entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
