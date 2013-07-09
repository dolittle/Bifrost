using System.Linq;
using Bifrost.Validation;
using FluentValidation.Results;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_Validator
{
    [Subject(typeof(Validator<>))]
    public class when_validating_a_complex_graph_with_model_rules_and_concepts : given.a_complex_object_graph_and_validator
    {
        static ValidationResult result;

        Because of = () => result = validator.Validate(parent);

        It should_have_a_validation_error_for_parent_Id = () => result.Errors.Any(e => e.PropertyName == "Id").ShouldBeTrue();
        It should_have_a_validation_error_for_parent_integer = () => result.Errors.Any(e => e.PropertyName == "SimpleIntegerProperty").ShouldBeTrue();
        It should_have_a_validation_error_for_parent_string = () => result.Errors.Any(e => e.PropertyName == "SimpleStringProperty").ShouldBeTrue();
        It should_have_a_validation_error_for_child_concept = () => result.Errors.Any(e => e.PropertyName == "Child.ChildConcept").ShouldBeTrue();
        It should_have_a_validation_error_for_child_integer = () => result.Errors.Any(e => e.PropertyName == "Child.ChildSimpleIntegerProperty").ShouldBeTrue();
        It should_have_a_validation_error_for_child_string = () => result.Errors.Any(e => e.PropertyName == "Child.ChildSimpleStringProperty").ShouldBeTrue();
        It should_have_a_validation_error_for_grandchild_concept = () => result.Errors.Any(e => e.PropertyName == "Child.Grandchild.GrandchildConcept").ShouldBeTrue();
        It should_have_a_validation_error_for_grandchild_integer = () => result.Errors.Any(e => e.PropertyName == "Child.Grandchild.GrandchildSimpleIntegerProperty").ShouldBeTrue();
        It should_have_a_validation_error_for_grandchild_string = () => result.Errors.Any(e => e.PropertyName == "Child.Grandchild.GrandchildSimpleStringProperty").ShouldBeTrue();
    }
}