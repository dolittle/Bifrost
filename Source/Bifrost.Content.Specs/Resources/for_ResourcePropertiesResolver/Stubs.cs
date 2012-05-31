using Bifrost.Content.Resources;

namespace Bifrost.Specs.Resources.for_ResourcePropertiesResolver
{
    public class ClassWithRegularProperties
    {
        public string Something { get; set; }
    }

    public class ClassHavingResources : IHaveResources
    {
        
    }

    public class ClassWithResourceProperties
    {
        public ClassHavingResources Resources { get; set; }
    }
}
