using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI_BILLING.Data;
using WebApplicationAPI_BILLING.Models;
using WebApplicationAPI_BILLING.Services;

namespace WebApplicationAPI_BILLING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPurchaseOrdenesCService _purchaseOrdenesCService;

        public OrdenesCController(ApplicationDbContext context, IPurchaseOrdenesCService purchaseOrdenesCService)
        {
            _context = context;
            _purchaseOrdenesCService = purchaseOrdenesCService;
        }

        // GET: api/OrdenesC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenC>>> GetOrdenesC()
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            return await _context.OrdenesC.Include(oi => oi.OrdenItems).ToListAsync();
        }

        // GET: api/OrdenCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenC>> GetOrdenC(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            // var OrdenC = await _context.OrdenesC.Include(oi => oi.OrdenItems).FindAsync(id);
            var OrdenC = await _context.OrdenesC.Include(oi => oi.OrdenItems)
                                     .FirstOrDefaultAsync(o => o.Id == id); // Asumiendo que el nombre del campo es 'Id'.

            if (OrdenC == null)
            {
                return NotFound();
            }

            return OrdenC;
        }

        // PUT: api/OrdenesC/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenC(int id, OrdenC OrdenC)
        {
            if (id != OrdenC.Id)
            {
                return BadRequest();
            }

            _context.Entry(OrdenC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenCExists(id))
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

        // POST: api/OrdenesC
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenC>> PostOrdenC(OrdenC OrdenC)
        {
            if (_context.OrdenesC == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrdenesC'  is null.");
            }

            // Recorremos cada detalle en la orden de compra
            foreach (var detalle in OrdenC.OrdenItems)
            {
                // Asignar el precio unitario del producto al detalle
                detalle.UnitPrecio  = await _purchaseOrdenesCService.CheckUnitPrecio (detalle);

                // Calcular el subtotal
                detalle.Subtotal = await _purchaseOrdenesCService.CalculateSubtotalOrdenItem(detalle);
            }

            // Asignar el total calculado a la orden de compra (si tienes una propiedad explicita para el total en tu modelo)
            OrdenC.PagoTotal = _purchaseOrdenesCService.CalcularTotalOrdenItems((List<OrdenItem>)OrdenC.OrdenItems);

            _context.OrdenesC.Add(OrdenC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenC", new { id = OrdenC.Id }, OrdenC);
        }

        // DELETE: api/OrdenesC/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenC(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            var OrdenC = await _context.OrdenesC.FindAsync(id);
            if (OrdenC == null)
            {
                return NotFound();
            }

            _context.OrdenesC.Remove(OrdenC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenCExists(int id)
        {
            return (_context.OrdenesC?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}