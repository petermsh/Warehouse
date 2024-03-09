using TaskApp.Interfaces;

namespace TaskApp.Services;

/// <summary>
/// Klasa odpowiedzialna za pobranie pliku na podstawie podanego url do miejsca podanego w parametrze newFilePath
/// </summary>
internal sealed class FileService : IFileService
{
    public async Task DownloadFile(string url, string newFilePath)
    {
        try
        {
            using var client = new HttpClient();
            await using var stream = await client.GetStreamAsync(url);
            await using var outputFileStream = new FileStream(newFilePath, FileMode.Create);
            await stream.CopyToAsync(outputFileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}