using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject.given
{
    public class a_write_once_expando_object_with_values
    {
        protected const string IntegerKey = "Integer";
        protected static dynamic values;

        Establish context = () => values = new WriteOnceExpandoObject(d => {
            d[IntegerKey] = 5;
        });
    }
}
