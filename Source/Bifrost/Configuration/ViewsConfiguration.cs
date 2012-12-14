using Bifrost.Views;
using Bifrost.Execution;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an <see cref="IViewsConfiguration"/>
    /// </summary>
    public class ViewsConfiguration : ConfigurationStorageElement, IViewsConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public override void Initialize(IContainer container)
        {
            base.Initialize(container);
        }
#pragma warning restore // Xml Comments
    }
}
