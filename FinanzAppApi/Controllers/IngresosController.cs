using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzAppApi.Models;
using FinanzAppApi.Context;


namespace FinanzAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngresosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingresos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingreso>>> GetIngresos()
        {
            // Trae todos los ingresos de la base de datos
            return await _context.Ingresos.Include(i => i.Usuario).Include(i => i.Cuenta).ToListAsync();
        }

        // GET: api/Ingresos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingreso>> GetIngreso(int id)
        {
            var ingreso = await _context.Ingresos.Include(i => i.Usuario).Include(i => i.Cuenta).FirstOrDefaultAsync(i => i.Id == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            return ingreso;
        }

        // PUT: api/Ingresos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso(int id, Ingreso ingreso)
        {
            if (id != ingreso.Id)
            {
                return BadRequest();
            }

            // Aquí puedes realizar validaciones adicionales si lo necesitas
            _context.Entry(ingreso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngresoExists(id))
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

        // POST: api/Ingresos
        [HttpPost]
        public async Task<ActionResult<Ingreso>> PostIngreso(Ingreso ingreso)
        {
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngreso), new { id = ingreso.Id }, ingreso);
        }

        // DELETE: api/Ingresos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.Id == id);
        }
    }
}

