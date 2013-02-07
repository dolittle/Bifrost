
namespace Bifrost.Web.Proxies.JavaScript
{
    public class Function : LanguageElement
    {
        public Function(params string[] dependencies)
        {
            Dependencies = dependencies;
            Body = new FunctionBody();
        }

        public string[] Dependencies { get; set; }

        public FunctionBody Body { get; private set; }

        public override void Write(ICodeWriter writer)
        {
            writer.Write("function(");
            for (var dependencyIndex = 0; dependencyIndex < Dependencies.Length; dependencyIndex++)
            {
                if (dependencyIndex != 0)
                    writer.Write(", ");

                writer.Write(Dependencies[dependencyIndex]);
            }
            writer.Write(") {{");
            writer.Newline();
            Body.Write(writer);
            writer.WriteWithIndentation("}}");
        }
    }
}
