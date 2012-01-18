using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown when a <see cref="ISaga"/> is not in a <see cref="IChapter"/>
	/// </summary>
    public class SagaNotInChapterException : Exception
    {
    }
}