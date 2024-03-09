using System.Data.SqlClient;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using Npgsql;
using TaskApp.Dtos;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Services;

/// <summary>
/// Klasa odpowiedzialna za operacje związane z Products
/// </summary>
internal sealed class ProductsService : IProductsService
{
    private readonly DapperContext _context;

    public ProductsService(DapperContext context)
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
        using var csvProducts = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            MissingFieldFound = null
        });
        csvProducts.Context.TypeConverterOptionsCache.GetOptions<bool>().NullValues.Add(string.Empty);
        
        
        var products = csvProducts.GetRecords<Product>()
            .Where(p=>p.IsWire == false)
            .Where(p => p.Shipping == "24h")
            .ToList();
        
        
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync("save_products", products.Select(p => new 
        {
            id = p.Id,
            sku = p.SKU,
            name = p.Name,
            ean = p.EAN,
            producername = p.ProducerName,
            category = p.Category,
            iswire = p.IsWire,
            defaultimage = p.DefaultImage,
            shipping = p.Shipping
        }));
    }

    /// <summary>
    /// Metoda odpowiedzialna za wyświetlenie szczegółów produktu na podstawie parametru sku
    /// </summary>
    /// <param name="sku"></param>
    /// <returns></returns>
    public async Task<ProductDto> GetProduct(string sku)
    {
        using var connection = _context.CreateConnection();
        var product = await connection.QueryFirstOrDefaultAsync<ProductDto>("SELECT * FROM get_product_details(@in_sku)", new { in_sku = sku });

        return product;
    }
}
