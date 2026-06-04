namespace E_learning_platform.Patterns.Bridge
{
    public class MobileRenderer : IRenderer
    {
        public string Render(string mediaType, string title)
        {
            return $"Playing {mediaType} '{title}' on Mobile App.";
        }
    }
}
