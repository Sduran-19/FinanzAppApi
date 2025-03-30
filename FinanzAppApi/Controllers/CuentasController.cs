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
    public class CuentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CuentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
        {
            // Devuelve todas las cuentas, incluyendo los detalles del usuario
            return await _context.Cuentas.Include(c => c.Usuario).ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(int id)
        {
            var cuenta = await _context.Cuentas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        // PUT: api/Cuentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(int id, Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCuenta", new { id = cuenta.Id }, cuenta);
            return CreatedAtAction(nameof(GetCuenta), new { id = cuenta.Id }, cuenta);
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaExists(int id)
        {
            return _context.Cuentas.Any(e => e.Id == id);
        }
    }
}
