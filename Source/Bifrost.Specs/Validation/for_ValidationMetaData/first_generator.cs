using System;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.for_ValidationMetaData
{
    public class first_generator : ICanGenerateValidationMetaData
    {
        public TypeMetaData type_meta_data_to_return;
        public bool generate_for_called;

        public TypeMetaData GenerateFor(Type typeForValidation)
        {
            generate_for_called = true;
            return type_meta_data_to_return;
        }
    }
}
