
﻿using WebApplicationAPI_BILLING.Data;
using WebApplicationAPI_BILLING.Models;

namespace WebApplicationAPI_BILLING.Services
{
    public class PurchaseOrdenesCService : IPurchaseOrdenesCService
    {
        private readonly ApplicationDbContext _context;
        public PurchaseOrdenesCService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Producto> GetProductoById(int productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
            {
                throw new Exception($"El producto con ID {productoId} no fue encontrado.");
            }
            return producto;
        }

        public async Task<decimal> CheckUnitPrecio (OrdenItem detalle)
        {
            var producto = await GetProductoById(detalle.ProductoId);
            detalle.UnitPrecio  = producto.UnitPrecio ;

            return (decimal)detalle.UnitPrecio ;
        }

        public async Task<decimal> CalculateSubtotalOrdenItem(OrdenItem item)
        {
            decimal UnitPrecio  = await CheckUnitPrecio (item);
            item.Subtotal = UnitPrecio  * item.Cantidad;

            return (decimal)item.Subtotal;
        }

        public decimal CalcularTotalOrdenItems(List<OrdenItem> items)
        {

            decimal total = 0;
            foreach (var item in items)
            {
                total += (decimal)item.Subtotal;
            }
            //decimal total = (decimal)items.Sum(item => item.Subtotal);
            return total;
        }
    }
}