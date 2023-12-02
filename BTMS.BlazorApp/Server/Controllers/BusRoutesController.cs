using BTMS.BlazorApp.Shared.Models;
using BTMS.BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTMS.BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusRoutesController : ControllerBase
    {
        private readonly BusDbContext _context;
        public BusRoutesController(BusDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusRoute>>> GetBusRoutes()
        {
            var data = await _context.BusRoutes.ToListAsync();
            return data;
        }
        [HttpGet("VM")]
        public async Task<ActionResult<IEnumerable<BusRouteViewModel>>> GetBusRouteViewModels()
        {
            var data = await _context.BusRoutes.ToListAsync();
            var model = data.Select(b => new BusRouteViewModel
            {
                BusRouteId = b.BusRouteId,
                RouteName = b.From + "-" + b.To,
                ApproximateDistance = b.ApproximateDistance,
                ApproximateJourneyHour = b.ApproximateJourneyHour,
            }).ToList();
            return model;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BusRoute>> GetBusRoute(int id)
        {
            var data = await _context.BusRoutes.FirstOrDefaultAsync(b => b.BusRouteId == id);
            if (data == null) { return NotFound(); }
            return data;
        }
        [HttpPost]
        public async Task<ActionResult<BusRoute>> PostBusRoute(BusRoute busRoute)
        {
            await this._context.BusRoutes.AddAsync(busRoute);
            await this._context.SaveChangesAsync();
            return busRoute;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBusRoute(int id, BusRoute busRoute)
        {
            if (id != busRoute.BusRouteId) return BadRequest("Ids font match");
            _context.Entry(busRoute).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            return NoContent();
        }
      
    }
}
