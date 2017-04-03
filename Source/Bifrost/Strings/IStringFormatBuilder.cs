/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Strings
{
    /// <summary>
    /// Defines a builder for building <see cref="IStringFormat"/>
    /// </summary>
    public interface IStringFormatBuilder
    {
        /// <summary>
        /// Build an instance of <see cref="IStringFormat"/> from the configuration
        /// </summary>
        /// <returns><see cref="IStringFormat"/> configured correctly</returns>
        IStringFormat Build();

        /// <summary>
        /// Adds a <see cref="FixedStringSegment">fixed string segment</see> to the <see cref="IStringFormat"/>
        /// </summary>
        /// <param name="string">The fixed string</param>
        /// <returns><see cref="IStringFormatBuilder"/> to continue building on</returns>
        IStringFormatBuilder FixedString(string @string);

        /// <summary>
        /// Adds a <see cref="FixedStringSegment">fixed string segment</see> to the <see cref="IStringFormat"/>
        /// </summary>
        /// <param name="string">The fixed string</param>
        /// <param name="callback">Callback to build the <see cref="FixedStringSegment"/> with a <see cref="IFixedStringSegmentBuilder"/></param>
        /// <returns></returns>
        IStringFormatBuilder FixedString(string @string, Func<IFixedStringSegmentBuilder, IFixedStringSegmentBuilder> callback);

        /// <summary>
        /// Adds a <see cref="VariableStringSegment">Variable string segment</see> to the <see cref="IStringFormat"/>
        /// </summary>
        /// <param name="variableName">The Variable string</param>
        /// <returns><see cref="IStringFormatBuilder"/> to continue building on</returns>
        IStringFormatBuilder VariableString(string variableName);

        /// <summary>
        /// Adds a <see cref="VariableStringSegment">Variable string segment</see> to the <see cref="IStringFormat"/>
        /// </summary>
        /// <param name="variableName">The Variable string</param>
        /// <param name="callback">Callback to build the <see cref="VariableStringSegment"/> with a <see cref="IVariableStringSegmentBuilder"/></param>
        /// <returns></returns>
        IStringFormatBuilder VariableString(string variableName, Func<IVariableStringSegmentBuilder, IVariableStringSegmentBuilder> callback);
    }
}
