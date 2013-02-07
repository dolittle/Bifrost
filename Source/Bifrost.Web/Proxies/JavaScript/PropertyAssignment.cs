namespace Bifrost.Web.Proxies.JavaScript
{
    public class PropertyAssignment : Assignment
    {
        public PropertyAssignment(string name, ILanguageElement value = null) : base(name, value)
        {
        }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("this.{0} = ", Name);
            if( Value != null ) Value.Write(writer);
            writer.Write(";");
            writer.Newline();
        }
    }
}
