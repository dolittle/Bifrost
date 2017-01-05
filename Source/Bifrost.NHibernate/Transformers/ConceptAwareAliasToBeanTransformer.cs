/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Reflection;
using NHibernate;
using NHibernate.Properties;
using NHibernate.Transform;

namespace Bifrost.NHibernate.Transformers
{
    [Serializable]
    public class ConceptAwareAliasToBeanResultTransformer : IResultTransformer
    {
        
        const BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        readonly Type _resultClass;
        ISetter[] _setters;
        readonly IPropertyAccessor _propertyAccessor;
        readonly ConstructorInfo _constructor;

        public ConceptAwareAliasToBeanResultTransformer(Type resultClass)
        {
            if (resultClass == null)
            {
                throw new ArgumentNullException("resultClass");
            }
            _resultClass = resultClass;

            _constructor = resultClass.GetConstructor(_bindingFlags, null, Type.EmptyTypes, null);

            // if resultClass is a ValueType (struct), GetConstructor will return null... 
            // in that case, we'll use Activator.CreateInstance instead of the ConstructorInfo to create instances
            if (_constructor == null && resultClass.IsClass)
            {
                throw new ArgumentException(
                    "The target class of a AliasToBeanResultTransformer need a parameter-less constructor",
                    "resultClass");
            }

            _propertyAccessor =
                new ChainedPropertyAccessor(new[]
                {
                    PropertyAccessorFactory.GetPropertyAccessor(null),
                    PropertyAccessorFactory.GetPropertyAccessor("field")
                });
        }

        public object TransformTuple(object[] tuple, String[] aliases)
        {
            if (aliases == null)
            {
                throw new ArgumentNullException("aliases");
            }
            object result;

            try
            {
                if (_setters == null)
                {
                    _setters = new ISetter[aliases.Length];
                    for (int i = 0; i < aliases.Length; i++)
                    {
                        string alias = aliases[i];
                        if (alias != null)
                        {
                            _setters[i] = _propertyAccessor.GetSetter(_resultClass, alias);
                        }
                    }
                }

                // if resultClass is not a class but a value type, we need to use Activator.CreateInstance
                result = _resultClass.IsClass
                    ? _constructor.Invoke(null)
                    : Activator.CreateInstance(_resultClass, true);

                for (int i = 0; i < aliases.Length; i++)
                {
                    if (_setters[i] != null)
                    {
                        _setters[i].Set(result, tuple[i]);
                    }
                }
            }
            catch (InstantiationException e)
            {
                throw new HibernateException("Could not instantiate result class: " + _resultClass.FullName, e);
            }
            catch (MethodAccessException e)
            {
                throw new HibernateException("Could not instantiate result class: " + _resultClass.FullName, e);
            }

            return result;
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConceptAwareAliasToBeanResultTransformer);
        }

        public bool Equals(ConceptAwareAliasToBeanResultTransformer other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return ReferenceEquals(this, other) || Equals(other._resultClass, _resultClass);
        }

        public override int GetHashCode()
        {
            return _resultClass.GetHashCode();
        }
    }
}