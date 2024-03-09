using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using Npgsql;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Services;

/// <summary>
/// Klasa odpowiedzialna za operacje związane z Prices
/// </summary>
internal sealed class PricesService : IPricesService
{
    private readonly DapperContext _context;

    public PricesService(DapperContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Metoda odpowiedzialna za pobranie danych z pliku Prices.csv oraz wysłanie ich do bazy danych
    /// </summary>
    /// <param name="filePath"></param>
    public async Task ProcessData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csvPrices = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = false,
            MissingFieldFound = null,
            BadDataFound = null
        });
        csvPrices.Context.RegisterClassMap<PriceMap>();

        var prices = csvPrices.GetRecords<Price>()
            .ToList();

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync("save_prices", prices.Select(p => new 
        {
            id = p.Id,
            sku = p.SKU,
            productprice = p.ProductPrice,
            productpricewithdisc = p.ProductPriceWithDiscount,
            unitpricewithdisc = p.ProductPriceWithDiscForUnit,
            vatrate = p.VatRate
        }));
    }
}