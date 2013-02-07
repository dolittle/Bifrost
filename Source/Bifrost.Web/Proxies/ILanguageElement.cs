using System.IO;
namespace Bifrost.Web.Proxies
{
    public interface ILanguageElement
    {
        void AddChild(ILanguageElement element);
        void Write(ICodeWriter writer);
    }
}
