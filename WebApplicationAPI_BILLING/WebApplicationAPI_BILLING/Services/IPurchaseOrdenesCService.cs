using WebApplicationAPI_BILLING.Models;

namespace WebApplicationAPI_BILLING.Services

{
    public interface IPurchaseOrdenesCService
    {
        Task<decimal> CheckUnitPrecio (OrdenItem detalle);
        Task<decimal> CalculateSubtotalOrdenItem(OrdenItem item);
        decimal CalcularTotalOrdenItems(List<OrdenItem> item);
    }
}