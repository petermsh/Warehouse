using CsvHelper.Configuration.Attributes;

namespace TaskApp.Models;

internal sealed class Product
{
    [Name("ID")]
    public string Id { get; set; }
    public string SKU { get; set; }
    [Name("name")]
    public string Name { get; set; }
    public string EAN { get; set; }
    [Name("producer_name")]
    public string ProducerName { get; set; }
    [Name("category")]
    public string Category { get; set; }
    [Name("is_wire")]
    public bool? IsWire { get; set; }
    [Name("shipping")]
    public string Shipping { get; set; }
    [Name("default_image")]
    public string? DefaultImage { get; set; }
}