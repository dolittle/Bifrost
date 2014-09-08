using Bifrost.Read;

namespace Bifrost.Specs.Rules.for_QueryValidationDescriptorFor
{
    public class SomeQuery : IQueryFor<SomeReadModel>
    {
        public int IntegerArgument { get; set; }
    }
}
