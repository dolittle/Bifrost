
namespace Bifrost.Web.Proxies.JavaScript
{
    public class FunctionBody : LanguageElement
    {
        public override void Write(ICodeWriter writer)
        {
            writer.Indent();
            WriteChildren(writer);
            writer.Unindent();
        }
    }
}
