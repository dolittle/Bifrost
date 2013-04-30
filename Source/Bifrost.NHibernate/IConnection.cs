using NHibernate;

namespace Bifrost.NHibernate
{
    /// <summary>
    /// Represents an NHibernate connection.  Provides access to the configured SessionFactory
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets an instance of ISessionFactory
        /// </summary>
       ISessionFactory SessionFactory { get; } 
    }
}