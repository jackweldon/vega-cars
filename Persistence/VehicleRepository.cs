using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.Models;
using vega.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vega.Extensions;

namespace vega.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;

        public VehicleRepository(VegaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await _context.Vehicles.FindAsync(id);
            }
            return await _context.Vehicles
            .Include(f => f.Features)
                .ThenInclude(vf => vf.Feature)
            .Include(f => f.Model)
                .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(v => v.Id == id);
        }


        public async Task AddVehicle(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            var query = _context.Vehicles.Include(f => f.Features)
                .ThenInclude(vf => vf.Feature).Include(v => v.Model).ThenInclude(m => m.Make.Models).AsQueryable();

            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId);
            }
            if (queryObj.ModelId.HasValue)
            {
                query = query.Where(v => v.Model.Id == queryObj.ModelId);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, Object>>>
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };
            result.TotalItems = await query.CountAsync();
            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();
            return result;
        }
    }
}