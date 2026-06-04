namespace E_learning_platform.Patterns.Bridge
{
    public class BrowserRenderer : IRenderer
    {
        public string Render(string mediaType, string title)
        {
            return $"Playing {mediaType} '{title}' in Web Browser.";
        }
    }
}
