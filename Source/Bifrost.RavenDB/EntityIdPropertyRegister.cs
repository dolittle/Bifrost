/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Raven.Client.Converters;

namespace Bifrost.RavenDB
{
    public class EntityIdPropertyRegister : IEntityIdPropertyRegister
    {
        readonly Dictionary<Type, PropertyInfo> _mappedIds = new Dictionary<Type, PropertyInfo>(); 
        readonly List<ITypeConverter> _typeConverters = new List<ITypeConverter>();

        public void RegisterIdProperty<T,TId>(Expression<Func<T,TId>> property)
        {
            if (property == null)
                throw new InvalidIdException("Cannot map an empty Id property expression");

            var propertyInfo = property.GetPropertyInfo();

            if(propertyInfo == null)
                throw new InvalidIdException("Can only map properties.  Expression provided is not a property.");

            if(_mappedIds.ContainsKey(typeof(T)))
                throw new DuplicateIdRegistrationForTypeException(string.Format("The type {0} is already registered with an Id property.",typeof(T).FullName));
            _mappedIds.Add(typeof(T), propertyInfo);
            BuildTypeConverterIfConcept<TId>();
        }

        void BuildTypeConverterIfConcept<T>()
        {
            var idType = typeof (T);
            if(IsConceptType(idType))
            {
                var conceptType = idType.BaseType.GetGenericArguments()[0];
                var conceptTypeConverter = typeof (ConceptTypeConverter<,>).MakeGenericType(new[] {idType, conceptType});
                _typeConverters.Add(Activator.CreateInstance(conceptTypeConverter) as ITypeConverter);
            }
        }

        bool IsConceptType(Type t)
        {
            if (t.BaseType != null && t.BaseType.IsGenericType)
            {
                var genericArgumentType = t.BaseType.GetGenericArguments()[0];
                var isConcept = typeof(ConceptAs<>).MakeGenericType(genericArgumentType).IsAssignableFrom(t);
                return isConcept;
            }
            return false;
        }

        public bool IsIdProperty(Type type, PropertyInfo propertyInfo)
        {
            PropertyInfo idProperty;
            if (!_mappedIds.TryGetValue(type, out idProperty))
                return false;

            return propertyInfo == idProperty;
        }

        public IEnumerable<ITypeConverter> GetTypeConvertersForConceptIds()
        {
            return _typeConverters.ToArray();
        }
    }
}