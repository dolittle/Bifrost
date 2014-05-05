using System;
using Bifrost.Execution;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Publishes exception to all <see cref="IExceptionSubscriber"/>
    /// </summary>
    public class ExceptionPublisher : IExceptionPublisher
    {
        private readonly ITypeImporter _typeImporter;

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionPublisher"/>
        /// </summary>
        /// <param name="typeImporter"></param>
        public ExceptionPublisher(ITypeImporter typeImporter)
        {
            _typeImporter = typeImporter;
        }

        /// <summary>
        /// Publishes the exception to all  <see cref="ExceptionPublisher"/>
        /// </summary>
        /// <param name="exception"></param>
        public void Publish(Exception exception)
        {
            var subscribers = _typeImporter.ImportMany<IExceptionSubscriber>();

            foreach (var subscriber in subscribers)
            {
                subscriber.Handle(exception);
            }
        }
    }
}
