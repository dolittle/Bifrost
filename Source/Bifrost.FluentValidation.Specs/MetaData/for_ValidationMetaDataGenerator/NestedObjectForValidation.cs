namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedObjectForValidation
    {
        public const string SomeObjectName = "someObject";
        public const string FirstLevelStringName = "firstLevelString";
        public ObjectForValidation SomeObject { get; set; }
        public string FirstLevelString { get; set; }

    }
}
