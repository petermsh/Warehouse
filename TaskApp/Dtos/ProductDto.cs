namespace TaskApp.Dtos;

public class ProductDto
{
    public string ProductName { get; set; }
    public string EAN { get; set; }
    public string ProducerName { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal NetProductCost { get; set; }
    public decimal ShippingCost { get; set; }
}