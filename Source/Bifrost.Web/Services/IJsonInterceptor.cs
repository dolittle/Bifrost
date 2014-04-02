namespace Bifrost.Web.Services
{
    public interface IJsonInterceptor
    {
        string Intercept(string json);
    }
}
