using BTMS.BlazorApp.Shared.InputModels;
using BTMS.BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTMS.BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaresController : ControllerBase
    {
        private readonly BusDbContext _context;
        public FaresController(BusDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fare>>> GetFares()
        {
            var data = await _context.Fares.ToListAsync();
            return data;
        }
        [HttpGet("Fares")]
        public async Task<ActionResult<IEnumerable<BusRoute>>> GetBusRouteFares()
        {
            var data = await _context.BusRoutes.Include(x=> x.Fares).ToListAsync();
            
            return data;
        }
        [HttpGet("id")]
        public async Task<ActionResult<Fare>> GetFare(int id)
        {
            var data = await _context.Fares.FirstOrDefaultAsync(b => b.FareId == id);
            if (data == null) { return NotFound(); }
            return data;
        }
        [HttpPost("VM")]
        public async Task<ActionResult> PostFareInputModel(FareInputModel model)
        {
            model.SeatFares.ForEach(async f =>
            {
                var fare = new Fare { BusRouteId = model.BusRouteId, BusType = model.BusType, SeatFare = f.SeatFare ?? 0, FareType = f.FareType, IsActive = f.IsActive };
                await _context.Fares.AddAsync(fare);
            });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
