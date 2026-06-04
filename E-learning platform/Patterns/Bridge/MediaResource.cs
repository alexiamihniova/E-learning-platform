namespace E_learning_platform.Patterns.Bridge
{
    public abstract class MediaResource
    {
        protected IRenderer _renderer;
        public string Title { get; set; }

        protected MediaResource(IRenderer renderer, string title)
        {
            _renderer = renderer;
            Title = title;
        }

        public abstract string Play();
    }
}
