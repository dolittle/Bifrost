
namespace Bifrost.Web.Proxies.JavaScript
{
    public class Function : LanguageElement
    {
        public Function(params string[] parameters)
        {
            Parameters = parameters;
            Body = new FunctionBody();
        }

        public string[] Parameters { get; set; }

        public FunctionBody Body { get; private set; }

        public override void Write(ICodeWriter writer)
        {
            writer.Write("function(");
            for (var parameterIndex = 0; parameterIndex < Parameters.Length; parameterIndex++)
            {
                if (parameterIndex != 0)
                    writer.Write(", ");

                writer.Write(Parameters[parameterIndex]);
            }
            writer.Write(") {{");
            writer.Newline();
            Body.Write(writer);
            writer.WriteWithIndentation("}}");
        }
    }
}
