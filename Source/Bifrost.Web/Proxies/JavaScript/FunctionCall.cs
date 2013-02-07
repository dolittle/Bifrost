namespace Bifrost.Web.Proxies.JavaScript
{
    public class FunctionCall : LanguageElement
    {
        public string Function { get; set; }
        public string[] Parameters { get; set; }

        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}(", Function);
            if (Parameters != null && Parameters.Length > 0)
            {
                var count = 0;
                foreach (var parameter in Parameters)
                {
                    if (count != 0) writer.Write(", ");

                    writer.Write(parameter);

                    count++;
                }
            }
            writer.Write(")");

        }
    }
}
