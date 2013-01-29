using System;
using Raven.Client.Converters;
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.RavenDB
{
    public class ConceptTypeConverter<T,TValue> : ITypeConverter where T : ConceptAs<TValue>, new() where TValue : IEquatable<TValue>
    {
        const string _doubleQuote = @"""";

        public bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof (T);
        }

        public string ConvertFrom(string tag, object value, bool allowNull)
        {
            if (value == null && allowNull)
                return null;

            var stringValue = value == null ? string.Empty : value.ToString();

            return string.Concat(Wrap(tag), ":", Wrap(stringValue));
        }

        public object ConvertTo(string value)
        {
            var keyValue = value.Split(':');
            var conceptValue = Unwrap(keyValue[1]).ParseTo(typeof (TValue));
            return new T {Value = (TValue)conceptValue};
        }

        string Wrap(string value)
        {
            return string.Concat(_doubleQuote, value, _doubleQuote);
        }

        string Unwrap(string value)
        {
            return value.TrimStart('"').TrimEnd('"');
        }
    }
}
