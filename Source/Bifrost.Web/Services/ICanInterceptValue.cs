namespace Bifrost.Web.Services
{
    public interface ICanInterceptValue<T>
    {
        T Intercept(T value);
    }
}
