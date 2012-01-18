

using Bifrost.Views;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an <see cref="IViewsConfiguration"/>
    /// </summary>
    public class ViewsConfiguration : IViewsConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public void Initialize(IConfigure configure)
        {
        }
#pragma warning restore // Xml Comments

        /// <summary>
        /// Gets and sets an instance of the <see cref="IEntityContextConfiguration"/>
        /// </summary>
        public IEntityContextConfiguration Storage { get; set; }
    }
}
