using Microsoft.AspNetCore.Mvc;
using TaskApp.Dtos;
using TaskApp.Interfaces;

namespace TaskApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IProductsService _productsService;
    private readonly IInventoryService _inventoryService;
    private readonly IPricesService _pricesService;

    public WarehouseController(IFileService fileService, IProductsService productsService, IInventoryService inventoryService, IPricesService pricesService, IConfiguration configuration)
    {
        _fileService = fileService;
        _productsService = productsService;
        _inventoryService = inventoryService;
        _pricesService = pricesService;
        _configuration = configuration;
    }

    /// <summary>
    /// Endpoint odpowiedzialny za pobranie plików i przesłanie ich do bazy danych
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> ProcessDataFromFiles()
    {
        var urls = _configuration.GetSection("Urls");
        var paths = _configuration.GetSection("Paths");
        
        await _fileService.DownloadFile(urls["ProductsUrl"], paths["NewProductFilePath"]);
        await _fileService.DownloadFile(urls["InventoryUrl"], paths["NewInventoryFilePath"]);
        await _fileService.DownloadFile(urls["PricesUrl"], paths["NewPricesFilePath"]);
        
        await _productsService.ProcessData(paths["NewProductFilePath"]);
        await _inventoryService.ProcessData(paths["NewInventoryFilePath"]);
        await _pricesService.ProcessData(paths["NewPricesFilePath"]);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint odpowiedzialny za pobranie szczegółów produktu na podstawie parametru sku
    /// </summary>
    /// <param name="sku"></param>
    /// <returns></returns>
    [HttpGet("{sku}")]
    public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] string sku)
    {
        var product = await _productsService.GetProduct(sku);

        return product;
    }
}