using System;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Publishes exception to all <see cref="IExceptionSubscriber"/>
    /// </summary>
    public interface IExceptionPublisher
    {

        /// <summary>
        /// Publishes the exception to all  <see cref="ExceptionPublisher"/>
        /// </summary>
        /// <param name="exception"></param>
        void Publish(Exception exception);
    }
}
