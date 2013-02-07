using System.Collections.Generic;
using System.IO;

namespace Bifrost.Web.Proxies
{
    public abstract class LanguageElement : ILanguageElement
    {
        protected List<ILanguageElement> Children { get; private set; }

        protected LanguageElement()
        {
            Children = new List<ILanguageElement>();
        }

        public ILanguageElement Parent { get; set; }

        public abstract void Write(ICodeWriter writer);

        public void AddChild(ILanguageElement element)
        {
            element.Parent = this;
            Children.Add(element);
        }

        protected void WriteChildren(ICodeWriter writer)
        {
            foreach (var child in Children)
                child.Write(writer);
        }

    }
}
