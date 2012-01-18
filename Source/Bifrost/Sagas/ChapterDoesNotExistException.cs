using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown if a <see cref="IChapter"/> does not exist in a <see cref="ISaga"/>
	/// </summary>
	public class ChapterDoesNotExistException : Exception
	{
		
	}
}