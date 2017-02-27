using Bifrost.Configuration;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_SagasConfiguration.given
{
    public class a_sagas_configuration
    {
        protected static SagasConfiguration sagas_configuration;

        Establish context = () =>
        {
            sagas_configuration = new SagasConfiguration();
        };
    }
}
