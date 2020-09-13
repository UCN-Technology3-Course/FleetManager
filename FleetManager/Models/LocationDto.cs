using FleetManager.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetManager.Models
{
    class LocationDto : BaseDto
    {
        public string Address { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public static LocationDto Create(Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Link = new ApiLink("/locations/{0}", location.Id),
                Address = location.Address,
                Zip = location.Zip,
                City = location.City,
                Phone = location.Phone
            };
        }

        public static IEnumerable<LocationDto> Create(IEnumerable<Location> locations)
        {
            foreach (var location in locations)
            {
                yield return Create(location);
            }
        }
    }
}