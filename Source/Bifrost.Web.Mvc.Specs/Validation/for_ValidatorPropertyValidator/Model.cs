using System.Reflection;

namespace Bifrost.Web.Mvc.Specs.Validation.for_ValidatorPropertyValidator
{
    public class Model
    {
        public static PropertyInfo TheStringProperty = typeof(Model).GetProperty("TheString");

        public bool TheStringGetCalled = false;
        public bool TheStringSetCalled = false;

        string _theString;
        public string TheString 
        {
            get
            {
                TheStringGetCalled = true;
                return _theString;
            }
            set
            {
                TheStringSetCalled = true;
                _theString = value;
            }
        }
    }
}
