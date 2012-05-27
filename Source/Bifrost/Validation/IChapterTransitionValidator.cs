using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Sagas;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a validator for a Saga <see href="IChapter">Chapter</see>
    /// </summary>
    public interface IChapterTransitionValidator
    {
        /// <summary>
        /// Validates that a chapter has all the requirements to allow transition.
        /// </summary>
        /// <param name="chapter">The chapter to validate</param>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid chapter.</returns>
        IEnumerable<ValidationResult> ValidateChapter(IChapter chapter);
    }
}