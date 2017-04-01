/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Strings
{
    /// <summary>
    /// Defines a parser that can parse to <see cref="IStringFormat"/>
    /// </summary>
    public interface IStringFormatParser
    {
        /// <summary>
        /// Parse a string for a <see cref="IStringFormat"/>
        /// </summary>
        /// <param name="format"><see cref="string"/> representing the <see cref="IStringFormat"/> - see remarks for more details</param>
        /// <returns><see cref="IStringFormat"/> representing the actual format</returns>
        /// <remarks>
        /// Expected format:
        /// 
        /// strings must start with a block identifying the segment separator 
        /// character(s); [(separators)] e.g. [.] if your string segments are 
        /// separated by a dot. 
        /// 
        /// Each string segment can have the following:
        /// 
        /// Fixed Strings are just fixed strings
        /// Variables must be enclosed in curly brackets - e.g. {MyVariable}
        /// The string within the brackets is considered the name of the variable
        /// 
        /// Prefixes:
        /// - - optional string
        /// ^ - Depending on previous
        /// 
        /// Postfixes:
        /// * - recurring
        /// 
        /// Example
        /// [.]SomeFixedString.{FirstVariable}.-{SecondOptionalVariable}.^{ThirdNonOptionalDependingOnPrevious}.-{FourthOptionalRecurringVariable}"
        /// </remarks>
        IStringFormat Parse(string format);
    }
}
