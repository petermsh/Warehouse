namespace TaskApp.Interfaces;

public interface IFileService
{
    public Task DownloadFile(string url, string newFilePath);
}