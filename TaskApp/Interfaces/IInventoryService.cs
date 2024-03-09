namespace TaskApp.Interfaces;

public interface IInventoryService
{
    public Task ProcessData(string filePath);
}