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