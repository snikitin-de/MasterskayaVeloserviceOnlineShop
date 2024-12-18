using Markdig;

namespace OnlineShopWebApp.Services
{
    public class MarkdownService
    {
        public string Parse(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder()
               .UseAdvancedExtensions()
               .Build();

            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
