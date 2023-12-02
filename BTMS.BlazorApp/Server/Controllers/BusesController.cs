using BTMS.BlazorApp.Shared.Models;
using BTMS.BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTMS.BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly BusDbContext _context;
        private readonly IWebHostEnvironment env;
        public BusesController(BusDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBuses()
        {
            var data = await _context.Buses.ToListAsync();
            return data;
        }
        [HttpGet("id")]
        public async Task<ActionResult<Bus>> GetBus(int id)
        {
            var data = await _context.Buses.FirstOrDefaultAsync(b=> b.BusId == id);
            if(data == null) { return NotFound(); }
            return data;
        }
        // GET: api/Buses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bus>> GetBusWithId(int id)
        {
            if (_context.Buses == null)
            {
                return NotFound();
            }
            var bus = await _context.Buses.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return bus;
        }
        // PUT: api/Buses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, Bus bus)
        {
            if (id != bus.BusId)
            {
                return BadRequest();
            }

            _context.Entry(bus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Buses
        [HttpPost]
        public async Task<ActionResult<Bus>> PostBus(Bus bus)
        {
            if (_context.Buses == null)
            {
                return Problem("Entity set 'BusDbContext.Buses'  is null.");
            }
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBus", new { id = bus.BusId }, bus);
        }

        // DELETE: api/Buses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            if (_context.Buses == null)
            {
                return NotFound();
            }
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("Upload")]
        public async Task<UploadResponse> Upload(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);
            string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            //string s = env.WebRootPath;
            string savePath = Path.Combine(env.WebRootPath, "Pictures", f);
            using FileStream fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return new UploadResponse { ImageName = f };
        }
        private bool BusExists(int id)
        {
            return (_context.Buses?.Any(e => e.BusId == id)).GetValueOrDefault();
        }
    }
}
