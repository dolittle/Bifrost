namespace Bifrost.Web.Proxies.JavaScript
{
    public class KeyAssignment : Assignment
    {
        public KeyAssignment(string key, ILanguageElement value = null) : base(key, value)
        {
        }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("{0} : ", Name);
            if( Value != null ) Value.Write(writer);
        }
    }
}
