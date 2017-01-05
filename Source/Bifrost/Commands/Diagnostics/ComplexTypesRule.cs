/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Diagnostics;

namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents a rule that will check if a <see cref="ICommand"/> has too many properties
    /// </summary>
    public class ComplexTypesRule : ITypeRuleFor<ICommand>
    {
#pragma warning disable 1591 // Xml Comments
        public void Validate(System.Type type, IProblems problems)
        {
            
        }
#pragma warning restore 1591 // Xml Comments
    }
}
