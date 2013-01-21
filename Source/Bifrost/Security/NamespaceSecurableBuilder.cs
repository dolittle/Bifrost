namespace Bifrost.Security
{
    /// <summary>
    /// Represents the <see cref="ISecurityBuilder"/> for namespaces
    /// </summary>
    public class NamespaceSecurableBuilder : SecurableBuilder<NamespaceSecurable>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NamespaceSecurableBuilder"/>
        /// </summary>
        /// <param name="securable"><see cref="NamespaceSecurable"/> to build</param>
        public NamespaceSecurableBuilder(NamespaceSecurable securable) : base(securable)
        {
        }
    }
}
