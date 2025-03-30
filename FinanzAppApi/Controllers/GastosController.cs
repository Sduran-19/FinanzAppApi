using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzAppApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanzAppApi.Context;

namespace FinanzAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GastosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Gastos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gasto>>> GetGastos()
        {
            // Trae todos los gastos de la base de datos
            return await _context.Gastos.Include(g => g.Usuario).Include(g => g.Cuenta).ToListAsync();
        }

        // GET: api/Gastos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gasto>> GetGasto(int id)
        {
            var gasto = await _context.Gastos.Include(g => g.Usuario).Include(g => g.Cuenta).FirstOrDefaultAsync(g => g.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            return gasto;
        }

        // PUT: api/Gastos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto(int id, Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return BadRequest();
            }

            // Aquí puedes realizar validaciones adicionales si lo necesitas
            _context.Entry(gasto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastoExists(id))
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

        // POST: api/Gastos
        [HttpPost]
        public async Task<ActionResult<Gasto>> PostGasto(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGasto), new { id = gasto.Id }, gasto);
        }

        // DELETE: api/Gastos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}

