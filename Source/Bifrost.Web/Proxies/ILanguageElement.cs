using System.IO;
namespace Bifrost.Web.Proxies
{
    public interface ILanguageElement
    {
        ILanguageElement Parent { get; set; }

        void AddChild(ILanguageElement element);
        void Write(ICodeWriter writer);
    }
}
