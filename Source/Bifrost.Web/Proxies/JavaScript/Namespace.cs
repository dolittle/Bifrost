namespace Bifrost.Web.Proxies.JavaScript
{
    public class Namespace : LanguageElement
    {
        public string Namespace { get; set; }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("Bifrost.namespace(\"{0}\", {{", Namespace);
            writer.Indent();
            WriteChildren(writer);
            writer.Unindent();
            writer.WriteWithIndentation("}});");
            writer.Newline();
        }
    }
}
