using BTMS.BlazorApp.Shared.Models;
using BTMS.BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTMS.BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusSchedulesController : ControllerBase
    {
        private readonly BusDbContext _context;
        public BusSchedulesController(BusDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusSchedule>>> GetBusSchedules()
        {
            var data = await _context.Schedules.ToListAsync();
            return data;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BusSchedule>> GetBusSchedule(int id)
        {
            var data = await _context.Schedules.FirstOrDefaultAsync(x=> x.BusScheduleId == id);
            if(data == null) { return  NotFound(); }
            return data;
        }
        [HttpGet("VM")]
        public async Task<ActionResult<IEnumerable<BusScheduleViewModel>>> GetBusScheduleViewModels()
        {
            var data = await _context.Schedules
                .Include(x=> x.Bus)
                .Include(x=> x.BusRoute)
                .ToListAsync();
            if (data == null) { return NotFound(); }
            var vmData = data.Select(s => new BusScheduleViewModel
            {
                BusScheduleId = s.BusScheduleId,
                Date = s.Date,
                Time = s.Time,
                BusName = s.Bus?.Name ?? string.Empty,
                RouteName = s.BusRoute?.From + "-"+ s.BusRoute?.To ?? "",
                BusType = s.Bus?.BusType ?? BusType.NonAc
            })
                .ToList();
            return vmData;
        }
        [HttpPost]
        public async Task<ActionResult<BusSchedule>> PostBusSchedule(BusSchedule busSchedule)
        {
            await _context.Schedules.AddAsync(busSchedule);
            await _context.SaveChangesAsync();
            return busSchedule;
        }
    }
}
