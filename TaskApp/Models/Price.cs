using CsvHelper.TypeConversion;

namespace TaskApp.Models;

internal sealed class Price
{
    public string Id { get; set; }
    public string SKU { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductPriceWithDiscount { get; set; }
    public int? VatRate { get; set; }
    public decimal ProductPriceWithDiscForUnit { get; set; }
}