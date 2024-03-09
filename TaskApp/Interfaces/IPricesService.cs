namespace TaskApp.Interfaces;

public interface IPricesService
{
    public Task ProcessData(string filePath);
}