/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents the concept of an area within an <see cref="IApplication"/>
    /// Think of this as a technical area. Other terminology would possible call this tier or
    /// in some cases concern.
    /// </summary>
    public class ApplicationArea : ConceptAs<string>
    {
        /// <summary>
        /// Converts a <see cref="string"/> to an <see cref="ApplicationArea"/>
        /// </summary>
        /// <param name="area"><see cref="string"/> representing the area</param>
        public static implicit operator ApplicationArea(string area)
        {
            return new ApplicationArea { Value = area };
        }
    }
}
