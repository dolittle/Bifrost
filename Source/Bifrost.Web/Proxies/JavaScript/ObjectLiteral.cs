
namespace Bifrost.Web.Proxies.JavaScript
{
    public class ObjectLiteral : LanguageElement
    {
        public override void Write(ICodeWriter writer)
        {
            var childIndex = 0;
            var numberOfChildren = Children.Count;

            foreach (var child in Children)
            {
                child.Write(writer);
                childIndex++;

                if (childIndex > 0 && childIndex < numberOfChildren)
                    writer.Write(",");
                writer.Newline();
            }
            
        }
    }
}
