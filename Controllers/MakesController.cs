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
    public class MakesController : Controller
    {
        public IMapper Mapper { get; set; }

        private VegaDbContext context;
        public MakesController(VegaDbContext _context, IMapper mapper)
        {
            this.Mapper = mapper;
            context = _context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync(); ;
            return Mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}