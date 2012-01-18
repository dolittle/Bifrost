using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// The exception that is thrown when an object is read only and one is writing to it
    /// </summary>
    public class ReadOnlyObjectException : ArgumentException
    {
    }
}
