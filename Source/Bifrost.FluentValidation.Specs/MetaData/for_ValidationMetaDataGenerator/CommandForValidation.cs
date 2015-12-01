using Bifrost.Commands;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandForValidation : Command
    {
        public const string SomeStringName = "someString";
        public const string SomeIntName = "someInt";

        public string SomeString { get; set; }
        public int SomeInt { get; set; }
    }
}
