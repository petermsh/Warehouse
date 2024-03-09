using TaskApp.Dtos;
using TaskApp.Models;

namespace TaskApp.Interfaces;

public interface IProductsService
{
    public Task ProcessData(string filePath);
    public Task<ProductDto> GetProduct(string sku);
}