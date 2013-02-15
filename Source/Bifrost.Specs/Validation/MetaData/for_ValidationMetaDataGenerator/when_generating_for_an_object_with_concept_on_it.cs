using Bifrost.Validation.MetaData;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    [Subject(typeof(ValidationMetaDataGenerator))]
    public class when_generating_for_an_object_with_concept_on_it : given.a_validation_meta_data_generator_with_common_rules
    {
        static ObjectWithConceptValidator    validator;
        static ValidationMetaData result;

        Establish context = () => 
        {
            validator = new ObjectWithConceptValidator();
        };

        Because of = () => result = generator.GenerateFrom(validator);

        It should_have_required_for_concept = () => result["stringConcept"]["required"].ShouldNotBeNull();
    }
}
