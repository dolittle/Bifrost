namespace Bifrost.Events.Azure.Tables
{
    /// <summary>
    /// Delegate for providing connection string for <see cref="EventStore"/>
    /// </summary>
    /// <returns></returns>
    public delegate string ICanProvideConnectionString();
}
