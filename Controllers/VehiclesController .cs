using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;

using Microsoft.AspNetCore.Authorization;
using vega.Core.Models;
using vega.Core;
using System.Linq;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        public IMapper Mapper { get; set; }

        private readonly IVehicleRepository repo;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper mapper, IVehicleRepository repo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = repo;
            this.Mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {

            var vehicle = await repo.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var vr = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vr);
        }

        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filtersResource)
        {
            var filter = Mapper.Map<VehicleQueryResource, VehicleQuery>(filtersResource);
            var queryResult = await repo.GetVehicles(filter);
            return Mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var model = await context.Models.FindAsync(vehicleReource.ModelId);
            // if (model == null)
            // {
            //     ModelState.AddModelError("ModelId", "Invalid ModelId");
            //     return BadRequest(ModelState);
            // }

            var vehicle = Mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            await repo.AddVehicle(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repo.GetVehicle(vehicle.Id);
            var res = Mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(res);
        }

        [HttpPut("{id}")]// /api/vehcile/{id}
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleReource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repo.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            Mapper.Map<SaveVehicleResource, Vehicle>(vehicleReource);

            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            var res = Mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repo.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
                return NotFound();
            repo.RemoveVehicle(vehicle);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }


    }
}