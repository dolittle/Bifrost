namespace Bifrost.Web.Proxies.JavaScript
{
    public class TypeExtension : Container
    {
        public TypeExtension(string superType="Bifrost.Type")
        {
            SuperType = superType;
            Function = new Function();
        }

        public string SuperType { get; set; }
        public Function Function { get; private set; }

        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}.extend(", SuperType);
            Function.Write(writer);
            writer.Write(")");
        }
    }
}
