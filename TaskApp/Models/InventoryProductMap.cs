using System.Globalization;
using CsvHelper.Configuration;

namespace TaskApp.Models;

/// <summary>
/// Klasa mapująca wiersze pliku csv na obiekty klasy InventoryProduct
/// </summary>
internal sealed class InventoryProductMap : ClassMap<InventoryProduct>
{
    private InventoryProductMap()
    {
        Map(m => m.ProductId).Index(0);
        Map(m => m.Unit).Index(2);
        Map(m => m.Qty).TypeConverterOption.NumberStyles(NumberStyles.Float).Index(3);
        Map(m => m.ShippingTime).Index(6);
        Map(m => m.ShippingCost).Index(7).TypeConverterOption.NumberStyles(NumberStyles.Float);
    }
}