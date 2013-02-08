namespace Bifrost.Web.Proxies.JavaScript
{
    public abstract class Assignment : LanguageElement
    {
        public Assignment(string name, ILanguageElement value = null)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public ILanguageElement Value { get; set; }

        public abstract override void Write(ICodeWriter writer);
    }
}
