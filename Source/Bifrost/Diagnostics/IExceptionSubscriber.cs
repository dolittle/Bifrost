using System;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Subscribes to exceptions
    /// </summary>
    public interface IExceptionSubscriber
    {
        /// <summary>
        /// Handles the exception
        /// </summary>
        /// <param name="exception"></param>
        void Handle(Exception exception);
    }
}
