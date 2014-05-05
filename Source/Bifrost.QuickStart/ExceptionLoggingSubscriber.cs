using System;
using Bifrost.Diagnostics;

namespace Bifrost.QuickStart
{
    public class ExceptionLoggingSubscriber : IExceptionSubscriber
    {
        public void Handle(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Exception: " + exception.Message);
        }
    }
}