using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Converters;
using Bifrost.Concepts;
using Bifrost.Extensions;
using System.ComponentModel;

namespace Bifrost.RavenDB
{
    public class ConceptTypeConverter : ITypeConverter
    {
        const string _format = "\"{0}\":\"{1}\",\"__type\":\"{2}\",\"__assembly\":\"{3}\"";
        const string _escapedQuote = "\"";

        public bool CanConvertFrom(Type sourceType)
        {
            if (sourceType.BaseType.IsGenericType)
            {
                var genericArgumentType = sourceType.BaseType.GetGenericArguments()[0];
                if (genericArgumentType.HasInterface(typeof(IEquatable<>)))
                {
                    var isConcept = typeof(ConceptAs<>).MakeGenericType(genericArgumentType).IsAssignableFrom(sourceType);
                    return isConcept;
                }
            }
            return false;
        }

        public string ConvertFrom(string tag, object value, bool allowNull)
        {
            if (value == null && allowNull)
                return null;

            var conceptType = value.GetType();

            return string.Format(_format, tag, value, conceptType.FullName, conceptType.Assembly.GetName().Name);

        }

        public object ConvertTo(string value)
        {
            var values = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var idValues = values[0].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var typeValues = values[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var assemblyValues = values[2].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            var type = Type.GetType(string.Format(@"{0},{1}",
                typeValues[1].Replace(_escapedQuote,string.Empty),assemblyValues[1].Replace(_escapedQuote,string.Empty)));
            var instance = Activator.CreateInstance(type);
            var genericArgumentType = type.BaseType.GetGenericArguments()[0];
            var conceptValueAsString = idValues[1].Replace(_escapedQuote, string.Empty);
            var conceptValue = conceptValueAsString.ParseTo(genericArgumentType);
            type.GetProperty("Value").SetValue(instance, conceptValue, null);

            return instance;
        }
    }
}
