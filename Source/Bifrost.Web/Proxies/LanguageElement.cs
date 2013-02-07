using System.Collections.Generic;
using System.IO;

namespace Bifrost.Web.Proxies
{
    public abstract class LanguageElement : ILanguageElement
    {
        List<ILanguageElement> _children = new List<ILanguageElement>();

        public abstract void Write(ICodeWriter writer);

        public void AddChild(ILanguageElement element)
        {
            _children.Add(element);
        }

        protected void WriteChildren(ICodeWriter writer)
        {
            foreach (var child in _children)
                child.Write(writer);
        }
    }
}
