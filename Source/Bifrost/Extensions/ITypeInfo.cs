
namespace Bifrost.Extensions
{
    /// <summary>
    /// Defines information for types
    /// </summary>
    public interface ITypeInfo
    {
        /// <summary>
        /// Gets a boolean indicating wether or not the type has a default constructor that takes no arguments
        /// </summary>
        bool HasDefaultConstructor { get; }
    }
}
