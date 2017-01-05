/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Diagnostics;

namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents a rule that will check if a <see cref="ICommand"/> has too many properties
    /// </summary>
    public class TooManyPropertiesRule : ITypeRuleFor<ICommand>
    {
#pragma warning disable 1591 // Xml Comments
        public void Validate(Type type, IProblems problems)
        {
            if (type == typeof(ICommand) || type == typeof(Command))
                return;

            if (type.GetTypeInfo().GetProperties().Length > 10)
                problems.Report(ProblemTypes.TooManyProperties, CommandProblemMetaData.From(type));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
