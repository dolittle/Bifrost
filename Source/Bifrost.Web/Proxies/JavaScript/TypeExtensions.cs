namespace Bifrost.Web.Proxies.JavaScript
{
    public static class TypeExtensions
    {
        public static TypeExtension WithSuper(this TypeExtension typeExtension, string super)
        {
            typeExtension.SuperType = super;
            return typeExtension;
        }
    }
}
