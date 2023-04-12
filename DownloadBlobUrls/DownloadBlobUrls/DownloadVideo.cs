using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadBlobUrls
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

      var blobData = Convert.FromBase64String(blobUrl.Substring(5));

      using (var stream = new MemoryStream(blobData))
      using (var outputStream = File.Open(outputPath, FileMode.Create))
      {
        await stream.CopyToAsync(outputStream);
      }
    }
  }
}
