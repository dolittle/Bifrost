using System.IO;
using System.Text;

namespace Bifrost.Web.Proxies
{
    public class CodeGenerator : ICodeGenerator
    {
        public string GenerateFrom(ILanguageElement languageElement)
        {
            var stringBuilder = new StringBuilder();
            var textWriter = new StringWriter(stringBuilder);
            var writer = new CodeWriter(textWriter);

            languageElement.Write(writer);

            return stringBuilder.ToString();
        }
    }
}
