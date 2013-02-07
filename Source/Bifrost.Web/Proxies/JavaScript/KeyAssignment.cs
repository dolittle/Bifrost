namespace Bifrost.Web.Proxies.JavaScript
{
    public class KeyAssignment : LanguageElement
    {
        public string Key { get; set; }
        public LanguageElement Value { get; set; }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("{0} : ", Key);
            Value.Write(writer);
        }
    }
}
