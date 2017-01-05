/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Diagnostics;
using Bifrost.Extensions;


namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents a rule that will check if a <see cref="ICommand"/> has too many properties
    /// </summary>
    public class CommandInheritanceRule : ITypeRuleFor<ICommand>
    {
#pragma warning disable 1591 // Xml Comments
        public void Validate(Type type, IProblems problems)
        {
            var implementsInterface = type.HasInterface<ICommand>() && type.GetTypeInfo().BaseType == typeof(Object);

            if (!implementsInterface && type.GetTypeInfo().BaseType != typeof(Command))
                problems.Report(ProblemTypes.CommandInheritance, CommandProblemMetaData.From(type));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
