
namespace Bifrost.Web.Proxies.JavaScript
{
    public class Literal : LanguageElement
    {
        public Literal(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override void Write(ICodeWriter writer)
        {
            writer.Write(Content);
        }
    }
}
