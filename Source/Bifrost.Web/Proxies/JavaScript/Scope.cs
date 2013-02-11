
namespace Bifrost.Web.Proxies.JavaScript
{
    public class Scope : LanguageElement
    {
        public Scope(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override void Write(ICodeWriter writer)
        {
            foreach (var child in Children)
            {
                writer.WriteWithIndentation("{0}.", Name);
                child.Write(writer);
                writer.Write(";");
                writer.Newline();
            }
        }
    }
}
