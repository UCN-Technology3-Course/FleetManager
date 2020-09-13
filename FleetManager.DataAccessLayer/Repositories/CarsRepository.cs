using FleetManager.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Repositories
{
    class CarsRepository : IRepository<Car>
    {
        private static List<Car> data = new List<Car>
        {
            new Car{ Id=1, Brand = "Ford", Fuel = Car.FuelType.Gasoline, KilometersDriven = 123000, PassengerCapacity= 5, LocationId = 1},
            new Car{ Id=2, Brand = "Skoda", Fuel= Car.FuelType.Diesel, KilometersDriven = 21001, PassengerCapacity=5, LocationId=2}
        };

        public IEnumerable<Car> GetAll()
        {
            return data;
        }

        public Car GetById(int id)
        {
            return data.SingleOrDefault(c => c.Id == id);
        }

        public int Insert(Car entity)
        {
            entity.Id = data.Select(c => c.Id).OrderByDescending(id => id).First() + 1;
            data.Add(entity);
            return entity.Id;
        }

        public bool Update(Car entity)
        {
            var car = data.SingleOrDefault(c => c.Id == entity.Id);
            if (car != null)
            {
                if (entity.KilometersDriven > car.KilometersDriven)
                {
                    car.KilometersDriven = entity.KilometersDriven;
                }
                if (entity.LocationId.HasValue)
                {
                    car.LocationId = entity.LocationId == 0 ? null : entity.LocationId;
                }
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var car = data.SingleOrDefault(c => c.Id == id);
            if (car != null)
            {
                return data.Remove(car);
            }
            return false;
        }
    }
}
