using System;

namespace Bifrost.Events
{
	/// <summary>
	/// The exception that is thrown when an <see cref="IEvent"/> is out of sequence in an <see cref="EventStream"/>
	/// </summary>
    public class EventOutOfSequenceException : ArgumentException
    {
    }
}
