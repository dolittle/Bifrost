using Bifrost.Commands;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedCommandForValidation : Command
    {
        public const string SomeObjectName = "someCommand";
        public const string FirstLevelStringName = "firstLevelString";
        public CommandForValidation SomeCommand { get; set; }
        public string FirstLevelString { get; set; }
    }
}