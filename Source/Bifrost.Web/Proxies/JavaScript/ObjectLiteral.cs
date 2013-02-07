
namespace Bifrost.Web.Proxies.JavaScript
{
    public class ObjectLiteral : LanguageElement
    {
        public override void Write(ICodeWriter writer)
        {
            var childIndex = 0;
            var numberOfChildren = Children.Count;

            writer.Write("{{");

            if (numberOfChildren > 0)
            {
                writer.Newline();
                writer.Indent();

                foreach (var child in Children)
                {
                    child.Write(writer);
                    childIndex++;

                    if (childIndex > 0 && childIndex < numberOfChildren)
                        writer.Write(",");
                    writer.Newline();
                }

                writer.Unindent();
                writer.WriteWithIndentation("}}");
            }
            else
            {
                writer.Write("}}");
            }
        }
    }
}
