using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Persistence;

namespace vega.Controllers
{
    public class FeaturesController
    {
        public IMapper Mapper { get; set; }

        private VegaDbContext context;
        public FeaturesController(VegaDbContext _context, IMapper mapper)
        {
            this.Mapper = mapper;
            context = _context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();

            return Mapper.Map<List<Features>, List<KeyValuePairResource>>(features);
        }
    }


}