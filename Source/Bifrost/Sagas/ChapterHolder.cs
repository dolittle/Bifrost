/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a holder for a <see cref="IChapter"/>, typically used for serializing a chapter
    /// </summary>
	public class ChapterHolder
	{
        /// <summary>
        /// Gets or sets the type of the chapter
        /// </summary>
        /// <remarks>
        /// Fully Assembly qualified name
        /// </remarks>
		public string Type { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the <see cref="IChapter"/>
        /// </summary>
		public string SerializedChapter { get; set; }
	}
}