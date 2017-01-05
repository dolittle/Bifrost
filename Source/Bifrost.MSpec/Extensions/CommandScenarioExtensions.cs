/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Commands;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.MSpec.Extensions
{
    public static class CommandScenarioExtensions
    {
        public static void ShouldBeASuccessfulScenario<T>(this CommandScenario<T> scenario) where T : class, ICommand
         {
             scenario.IsSuccessful().ShouldBeTrue();
         }

        public static void ShouldBeAnUnsuccessfulScenario<T>(this CommandScenario<T> scenario) where T : class, ICommand
         {
             scenario.IsUnsuccessful().ShouldBeTrue();
         }
    }

}