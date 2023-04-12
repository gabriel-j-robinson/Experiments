using System.Text;
using Azure.Storage.Blobs;
namespace DownloadVideosLibrary
{
  public static class DownloadVideo
  {
    public static async Task DownloadVideoAsyncV1(string blobUrl, string outputPath)
    {
      using (var httpClient = new HttpClient())
      {
        var response = await httpClient.GetAsync(blobUrl);

        if (!response.IsSuccessStatusCode)
        {
          throw new Exception($"Failed to download video: {response.StatusCode}");
        }

        using (var stream = await response.Content.ReadAsStreamAsync())
        using (var outputStream = File.Open(outputPath, FileMode.Create))
        {
          await stream.CopyToAsync(outputStream);
        }
      }
    }

    public static async Task DownloadBlobVideoAsyncV2(string blobUrl, string outputPath)
    {
      if (!blobUrl.StartsWith("blob:", StringComparison.OrdinalIgnoreCase))
      {
        throw new ArgumentException("The URL is not a Blob URL.", nameof(blobUrl));
      }

      var blobData = Encoding.ASCII.GetBytes(blobUrl.Substring(5));

      using (var stream = new MemoryStream(blobData))
      using (var outputStream = File.Open(outputPath, FileMode.Create))
      {
        await stream.CopyToAsync(outputStream);
      }
    }

    public static async Task DownloadBlobVideoWithBlobClient(string blobUrl, string outputPath)
    {
      BlobClient blobClient = new BlobClient(new Uri(blobUrl));
      try
      {
        var response = await blobClient.DownloadContentAsync();
        if (response != null)
        {
          //using (var stream = response.Content.ToStream())
          //using (var outputStream = File.Open(outputPath, FileMode.Create))
          //{
          //  await stream.CopyToAsync(outputStream);
          //}
        }
      }
      catch (DirectoryNotFoundException ex)
      {
        // Let the user know that the directory does not exist
        Console.WriteLine($"Directory not found: {ex.Message}");
      }
    }
  }
}