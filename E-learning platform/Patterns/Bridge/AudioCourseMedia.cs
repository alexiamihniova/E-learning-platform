namespace E_learning_platform.Patterns.Bridge
{
    public class AudioCourseMedia : MediaResource
    {
        public AudioCourseMedia(IRenderer renderer, string title) : base(renderer, title)
        {
        }

        public override string Play()
        {
            return _renderer.Render("Audio", Title);
        }
    }
}
