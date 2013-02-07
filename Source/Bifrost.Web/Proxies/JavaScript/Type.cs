namespace Bifrost.Web.Proxies.JavaScript
{
    public class Type : LanguageElement
    {
        public Type()
        {
            SuperType = "Bifrost.Type";
        }

        public string SuperType { get; set; }
        public string[] Dependencies { get; set; }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("{0}.extend(function(", SuperType);
            for (var dependencyIndex = 0; dependencyIndex < Dependencies.Length; dependencyIndex++)
            {
                if (dependencyIndex != 0)
                    writer.WriteWithIndentation(", ");

                writer.WriteWithIndentation(Dependencies[dependencyIndex]);
            }
            writer.Write(") {");
            writer.Newline();
            writer.Indent();
            WriteChildren(writer);
            writer.Unindent();
            writer.WriteWithIndentation("});");
            writer.Newline();
        }
    }
}
