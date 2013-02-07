namespace Bifrost.Web.Proxies.JavaScript
{
    public class This : LanguageElement
    {
        public override void Write(ICodeWriter writer)
        {
            writer.Write("this");
        }
    }
}
