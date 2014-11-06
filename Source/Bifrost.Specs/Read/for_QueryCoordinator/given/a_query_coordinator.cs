using Bifrost.Read;
using Bifrost.Read.Validation;
using Bifrost.Rules;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator : all_dependencies
    {
        protected static QueryCoordinator coordinator;
        protected static QueryValidationResult validation_result;

        Establish context = () =>
        {
            fetching_security_manager_mock.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(new AuthorizationResult());
            validation_result = new QueryValidationResult(new BrokenRule[0]);
            query_validator_mock.Setup(q => q.Validate(Moq.It.IsAny<IQuery>())).Returns(validation_result);
            
            coordinator = new QueryCoordinator(
                type_discoverer_mock.Object, 
                container_mock.Object, 
                fetching_security_manager_mock.Object, 
                query_validator_mock.Object,
                read_model_filters_mock.Object);
        };
    }
}
