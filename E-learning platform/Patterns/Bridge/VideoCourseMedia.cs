namespace E_learning_platform.Patterns.Bridge
{
    public class VideoCourseMedia : MediaResource
    {
        public VideoCourseMedia(IRenderer renderer, string title) : base(renderer, title)
        {
        }

        public override string Play()
        {
            return _renderer.Render("Video", Title);
        }
    }
}
