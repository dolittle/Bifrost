namespace Bifrost.Web.Proxies.JavaScript
{
    public class Namespace : LanguageElement
    {
        public Namespace(string name)
        {
            Name = name;
            Content = new ObjectLiteral();
            Content.Parent = this;
        }

        public string Name { get; private set; }
        public ObjectLiteral Content { get; private set; }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("Bifrost.namespace(\"{0}\", {{", Name);
            writer.Newline();
            writer.Indent();
            Content.Write(writer);
            writer.Unindent();
            writer.WriteWithIndentation("}});");
            writer.Newline();
        }
    }
}
