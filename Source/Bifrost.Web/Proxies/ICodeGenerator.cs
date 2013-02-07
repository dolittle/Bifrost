namespace Bifrost.Web.Proxies
{
    public interface ICodeGenerator
    {
        string GenerateFrom(ILanguageElement languageElement);
    }
}
