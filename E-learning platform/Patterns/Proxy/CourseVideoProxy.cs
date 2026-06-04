using System;

namespace E_learning_platform.Patterns.Proxy
{
    public class CourseVideoProxy : ICourseVideo
    {
        private RealCourseVideo? _realVideo;
        private readonly string _videoUrl;
        private readonly bool _hasAccess;

        public CourseVideoProxy(string videoUrl, bool hasAccess)
        {
            _videoUrl = videoUrl;
            _hasAccess = hasAccess;
        }

        public string DisplayVideo()
        {
            if (!_hasAccess)
            {
                return "Access Denied. You must be enrolled or have a premium subscription to view this video.";
            }

            if (_realVideo == null)
            {
                _realVideo = new RealCourseVideo(_videoUrl);
            }

            return _realVideo.DisplayVideo();
        }
    }
}
