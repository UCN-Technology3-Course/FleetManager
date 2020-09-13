using FleetManager.DataAccessLayer;
using FleetManager.DataAccessLayer.Model;
using FleetManager.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FleetManager.Controllers
{
    public class CarsController : ApiController
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly Uri _baseUri = new Uri("http://localhost:44319");

        public CarsController(IRepository<Car> carRepository, IRepository<Location> locationRepository)
        {
            _carRepository = carRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("cars")]
        public IHttpActionResult GetAll()
        {
            var cars = _carRepository.GetAll();
            if (cars.Any())
            {
                return Content(HttpStatusCode.OK, CarDto.Create(cars));
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("cars/{id}/location")]
        public IHttpActionResult GetLocation(int id)
        {
            var car = _carRepository.GetById(id);
            if (car != null && car.LocationId.HasValue)
            {
                var location = _locationRepository.GetById(car.LocationId.Value);
                if (location != null)
                {
                    return Content(HttpStatusCode.OK, LocationDto.Create(location));
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("cars/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var car = _carRepository.GetById(id);
            if (car != null)
            {
                return Content(HttpStatusCode.OK, CarDto.Create(car));
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("cars")]
        public IHttpActionResult Create([FromBody] CarDto car)
        {
            Enum.TryParse(car.Fuel, out Car.FuelType fuel);

            var id = _carRepository.Insert(new Car
            {
                Brand = car.Brand,
                Fuel = fuel,
                KilometersDriven = car.KilometersDriven.HasValue ? car.KilometersDriven.Value : 0,
                PassengerCapacity = car.PassengerCapacity.HasValue ? car.PassengerCapacity.Value : 0,
            });

            if (id > 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(_baseUri, string.Format("/cars/{0}", id));
                return ResponseMessage(response);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [Route("cars/{id}")]
        public IHttpActionResult Update(int id, [FromBody] CarDto dto)
        {
            var car = new Car()
            {
                Id = id,
                KilometersDriven = dto.KilometersDriven.HasValue ? dto.KilometersDriven.Value : 0,
            };          
            var result = _carRepository.Update(car);
            return StatusCode(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpPut]
        [Route("cars/{id}/location")]
        public IHttpActionResult SetLocation(int id, [FromBody] CarDto.LocationDto dto)
        {
            var car = new Car()
            {
                Id = id,
                LocationId = dto.Id,
            };
            var result = _carRepository.Update(car);
            return StatusCode(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Route("cars/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _carRepository.Delete(id);
            return StatusCode(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }


        [HttpDelete]
        [Route("cars/{id}/location")]
        public IHttpActionResult RemoveLocation(int id)
        {
            var car = new Car()
            {
                Id = id,
                LocationId = 0,
            };
            var result = _carRepository.Update(car);
            return StatusCode(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
