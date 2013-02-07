namespace Bifrost.Web.Proxies.JavaScript
{
    public class VariantAssignment : Assignment
    {
        public VariantAssignment(string name, ILanguageElement value = null)
            : base(name, value)
        {
        }

        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("var {0} = ", Name);
            if( Value != null ) Value.Write(writer);
            writer.Write(";");
            writer.Newline();
        }
    }
}
