using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Services;

/// <summary>
/// Klasa odpowiedzialna za operacje związane z InventoryProducts
/// </summary>
internal sealed class InventoryService : IInventoryService
{
    private readonly DapperContext _context;

    public InventoryService(DapperContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metoda odpowiedzialna za pobranie danych z pliku Products.csv oraz wysłanie ich do bazy danych
    /// </summary>
    /// <param name="filePath"></param>
    public async Task ProcessData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csvInventoryProducts = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            MissingFieldFound = null
        });
        csvInventoryProducts.Context.RegisterClassMap<InventoryProductMap>();

        var inventoryProducts = csvInventoryProducts.GetRecords<InventoryProduct>()
            .Where(i => i.ShippingTime == "24h")
            .ToList();

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync("save_inventory_products", inventoryProducts.Select(i => new 
        {
            productid = i.ProductId,
            unit = i.Unit,
            quantity = i.Qty,
            shippingtime = i.ShippingTime,
            shippingcost = i.ShippingCost
        }));
    }
}