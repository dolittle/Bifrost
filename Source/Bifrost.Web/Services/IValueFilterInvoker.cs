namespace Bifrost.Web.Services
{
    public interface IValueFilterInvoker
    {
        string FilterInputValue(string value);
        string FilterOutputValue(string value);
    }
}
