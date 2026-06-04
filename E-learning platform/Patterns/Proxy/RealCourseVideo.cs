namespace E_learning_platform.Patterns.Proxy
{
    public class RealCourseVideo : ICourseVideo
    {
        private readonly string _videoUrl;

        public RealCourseVideo(string videoUrl)
        {
            _videoUrl = videoUrl;
            LoadVideoFromDisk();
        }

        private void LoadVideoFromDisk()
        {
            // Simulate heavy loading operation
            System.Console.WriteLine($"Loading video from {_videoUrl}...");
        }

        public string DisplayVideo()
        {
            return $"Playing video located at {_videoUrl}";
        }
    }
}
