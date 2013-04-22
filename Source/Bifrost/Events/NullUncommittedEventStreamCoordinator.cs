namespace Bifrost.Events
{
    /// <summary>
    /// A null implementation for <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    public class NullUncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream eventStream)
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}