using DownloadVideosLibrary;

namespace DownloadVideoUnitTests
{
  [TestClass]
  public class DownloadVideoTest
  {
    [TestMethod]
    public void TestDownloadVideoV1()
    {
      var task = Task.Run(() => DownloadVideo.DownloadVideoAsyncV1("blob:https://twitter.com/f9b663af-6a8c-4623-8214-1fb8bf9e65ba", "C:\\Users\\gabri\\Downloads\\video.mp4"));
      task.Wait();
    }

    [TestMethod]
    public void TestDownloadVideoV2()
    {
      var task = Task.Run(() => DownloadVideo.DownloadBlobVideoAsyncV2("blob:https://twitter.com/f9b663af-6a8c-4623-8214-1fb8bf9e65ba", "C:\\Users\\gabri\\Downloads\\video.mp4"));
      task.Wait();
    }

    [TestMethod]
    public void TestDownloadVideoWithBlobClient()
    {
      var task = Task.Run(() => DownloadVideo.DownloadBlobVideoWithBlobClient("blob:https://twitter.com/f9b663af-6a8c-4623-8214-1fb8bf9e65ba", "C:\\Users\\gabri\\Downloads\\video.mp4"));
      task.Wait();
    }
  }
}