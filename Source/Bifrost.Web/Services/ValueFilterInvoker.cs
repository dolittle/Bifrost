using Bifrost.Execution;

namespace Bifrost.Web.Services
{
    [Singleton]
    public class ValueFilterInvoker : IValueFilterInvoker
    {
        private readonly IInputValueFilter[] _inputValueFilters;
        private readonly IOutputValueFilter[] _outputValueFilters;

        public ValueFilterInvoker(ITypeImporter typeImporter)
        {
            _inputValueFilters = typeImporter.ImportMany<IInputValueFilter>();
            _outputValueFilters = typeImporter.ImportMany<IOutputValueFilter>();
        }

        public string FilterInputValue(string value)
        {
            string filteredValue = value;

            foreach (var valueFilter in _inputValueFilters)
            {
                filteredValue = valueFilter.Filter(filteredValue);
            }

            return filteredValue;
        }

        public string FilterOutputValue(string value)
        {
            string filteredValue = value;

            foreach (var valueFilter in _outputValueFilters)
            {
                filteredValue = valueFilter.Filter(filteredValue);
            }

            return filteredValue;
        }
    }
}