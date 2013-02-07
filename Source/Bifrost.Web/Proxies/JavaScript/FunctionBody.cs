
namespace Bifrost.Web.Proxies.JavaScript
{
    public class FunctionBody : Container
    {
        public override void Write(ICodeWriter writer)
        {
            writer.Indent();
            WriteChildren(writer);
            writer.Unindent();
        }
    }
}
