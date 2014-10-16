using Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_BusinessValidator.given
{
    public class a_complex_object_graph_and_validator
    {
        protected static ParentValidator validator;
        protected static Parent parent;

        Establish context = () =>
            {
                ValidatorOptions.PropertyNameResolver = NameResolvers.PropertyNameResolver;
                ValidatorOptions.DisplayNameResolver = NameResolvers.DisplayNameResolver;

                parent = new Parent
                    {
                        Id = -1,
                        SimpleIntegerProperty = 11,
                        SimpleStringProperty = "",
                        Child = new Child
                            {
                                ChildConcept = -2,
                                ChildSimpleIntegerProperty = 12,
                                ChildSimpleStringProperty = "",
                                Grandchild = new Grandchild
                                    {
                                        GrandchildConcept = -3,
                                        GrandchildSimpleIntegerProperty = 13,
                                        GrandchildSimpleStringProperty = ""
                                    }
                            }
                    };

                validator = new ParentValidator();
            };
    }

    public class Parent
    {
        public string SimpleStringProperty { get; set; }
        public int SimpleIntegerProperty { get; set; }
        public ConceptAsLong Id { get; set; }
        public Child Child { get; set; }
    }

    public class Child
    {
        public ConceptAsLong ChildConcept { get; set; }
        public string ChildSimpleStringProperty { get; set; }
        public int ChildSimpleIntegerProperty { get; set; }
        public Grandchild Grandchild { get; set; }
    }

    public class Grandchild
    {
        public ConceptAsLong GrandchildConcept { get; set; }
        public string GrandchildSimpleStringProperty { get; set; }
        public int GrandchildSimpleIntegerProperty { get; set; }
    }

    public class GrandchildValidator : BusinessValidator<Grandchild>
    {
        public GrandchildValidator()
        {
            RuleFor(gc => gc.GrandchildConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(gc => gc.GrandchildSimpleStringProperty)
                .NotEmpty();
            RuleFor(gc => gc.GrandchildSimpleIntegerProperty)
                .LessThan(10);
        }
    }

    public class ChildValidator : BusinessValidator<Child>
    {
        public ChildValidator()
        {
            RuleFor(c => c.ChildConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(c => c.ChildSimpleStringProperty)
                .NotEmpty();
            RuleFor(c => c.ChildSimpleIntegerProperty)
                .LessThan(10);
            RuleFor(c => c.Grandchild)
                .SetValidator(new GrandchildValidator());
        }
    }

    public class ParentValidator : BusinessValidator<Parent>
    {
        public ParentValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(p => p.SimpleStringProperty)
                .NotEmpty();
            RuleFor(p => p.SimpleIntegerProperty)
                .LessThan(10);
            RuleFor(p => p.Child)
                .SetValidator(new ChildValidator());
        }
    }
}