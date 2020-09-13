using FleetManager.DataAccessLayer;
using FleetManager.DataAccessLayer.Model;
using FleetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FleetManager.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationsController(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("locations")]
        public IHttpActionResult GetAll()
        {
            var data = LocationDto.Create(_locationRepository.GetAll());
            if (data.Any())
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, data);
                return ResponseMessage(response);
            }
            return StatusCode(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("locations/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var data = LocationDto.Create(_locationRepository.GetById(id));
            if (data != null)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, data);
                return ResponseMessage(response); 
            }
            return StatusCode(HttpStatusCode.NotFound);
        }
    }
}
