using CsvHelper.TypeConversion;

namespace TaskApp.Models;

internal sealed class InventoryProduct
{
    public string ProductId { get; set; }
    public string Unit { get; set; }
    public decimal Qty { get; set; }
    public string ShippingTime { get; set; }
    public decimal? ShippingCost { get; set; }
}