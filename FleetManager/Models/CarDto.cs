using FleetManager.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetManager.Models
{
    public class CarDto : BaseDto
    {
        public string Brand { get; set; }

        public string Fuel { get; set; }

        public int? PassengerCapacity { get; set; }

        public int? KilometersDriven { get; set; }

        public LocationDto Location { get; set; }

        public class LocationDto : BaseDto
        {

        }

        public static CarDto Create(Car car)
        {
            if (car == null)
            {
                return null;
            }

            return new CarDto
            {
                Id = car.Id,
                Link = new ApiLink("/cars/{0}", car.Id),
                Brand = car.Brand,
                Fuel = Enum.GetName(typeof(Car.FuelType), car.Fuel),
                KilometersDriven = car.KilometersDriven,
                PassengerCapacity = car.PassengerCapacity,
                Location = car.LocationId.HasValue ? new LocationDto
                {
                    Id = car.LocationId.Value,
                    Link = new ApiLink("/cars/{0}/location", car.Id, "car location")
                } : null
            };
        }

        public static IEnumerable<CarDto> Create(IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                yield return Create(car);
            }
        }


    }
}