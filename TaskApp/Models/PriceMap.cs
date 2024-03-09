using System.Globalization;
using CsvHelper.Configuration;
using TaskApp.Common;

namespace TaskApp.Models;

/// <summary>
/// Klasa mapująca wiersze pliku csv na obiekty klasy Price
/// </summary>
internal sealed class PriceMap : ClassMap<Price>
{
    private PriceMap()
    {
        Map(m => m.Id).Index(0);
        Map(m => m.SKU).Index(1);
        Map(m => m.ProductPrice)
            .TypeConverterOption.NumberStyles(NumberStyles.Number).Default(decimal.Zero)
            .Index(2);
        Map(m => m.ProductPriceWithDiscount)
            .TypeConverterOption.NumberStyles(NumberStyles.Number).Default(decimal.Zero)
            .Index(3);
        Map(m => m.VatRate)
            .TypeConverter(new TypeConverter())
            .Default(0)
            .Index(4);
        Map(m => m.ProductPriceWithDiscForUnit)
            .TypeConverterOption.NumberStyles(NumberStyles.Number).Default(decimal.Zero)
            .Index(5);
    }
}