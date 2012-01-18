using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown if a <see cref="IChapter"/> already exists within a <see cref="ISaga"/>
	/// </summary>
    public class ChapterAlreadyExistException : Exception
    {
        
    }
}