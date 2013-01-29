using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Raven.Client.Converters;

namespace Bifrost.RavenDB
{
    public class NullIdPropertyRegister : IEntityIdPropertyRegister
    {
        public void RegisterIdProperty<T,TId>(Expression<Func<T,TId>> property)
        { }

        public bool IsIdProperty(Type type, PropertyInfo propertyInfo)
        {
            return false;
        }

        public IEnumerable<ITypeConverter> GetTypeConvertersForConceptIds()
        {
            yield break;
        }
    }
}