using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Concepts;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter.given
{
    public class a_concept_type_converter
    {
        protected static ConceptTypeConverter converter;

        public a_concept_type_converter()
        {
            converter = new ConceptTypeConverter();
        }
    }

    public class ConceptAsGuid : ConceptAs<Guid>
    { }

    public class ConceptAsString : ConceptAs<String>
    { }

    public class ConceptAsLong : ConceptAs<long>
    { }
}
