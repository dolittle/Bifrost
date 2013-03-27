#region License

//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Based on CompositeUserTypeBase<> from http://codeinsanity.com/archive/2009/02/12/using-fluent-nhibernate-in-rhinestone-part-ii.aspx
    /// which was relased under the Apache 2.0 license
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UserTypeBase<T> : ICompositeUserType
    {
        static NullSafeMapping defaultMapping = new InferredMapping();

        readonly List<KeyValuePair<PropertyInfo,NullSafeMapping>> _mappedProperties = new List<KeyValuePair<PropertyInfo, NullSafeMapping>>();
        /// <summary>
        /// Maps a property for the composite user type.  Can only map properties, not fields.
        /// </summary>
        /// <param name="property">A expression representing the property to map.</param>
        /// <param name="property">A custom mapping to use when Getting and Setting a Property</param>
        protected virtual void MapProperty<U>(Expression<Func<T, U>> property, NullSafeMapping mapping)
        {
            if (property == null)
                throw new ArgumentNullException("property", "Cannot map an empty property expression");

            var propertyInfo = GetPropertyInfo(property);

            _mappedProperties.Add(new KeyValuePair<PropertyInfo, NullSafeMapping>(propertyInfo,mapping));
        }

        /// <summary>
        /// Maps a property for the composite user type.  Can only map properties, not fields.
        /// </summary>
        /// <param name="property">A expression representing the property to map.</param>
        protected virtual void MapProperty<U>(Expression<Func<T, U>> property)
        {
            MapProperty(property,defaultMapping);
        }

        PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("'{0}' expression is a method not a property.", propertyLambda));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("'{0}' expression uses a field, not a property.", propertyLambda));

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda, type));

            return propInfo;
        }

        /// <summary>
        /// Inheriting classes must construct the object from the values passed in.
        /// </summary>
        /// <param name="propertyValues">An array of values of type object retrieved from the database</param>
        /// <returns>An instance of Type T</returns>
        protected abstract T CreateInstance(object[] propertyValues);

        /// <summary>
        /// Inheriting classes must perform a deep copy of the instance.
        /// </summary>
        /// <param name="source">An instance of type T that should be deep copied</param>
        /// <returns>An instance of Type T, copied from source</returns>
        protected abstract T PerformDeepCopy(T source);

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <param name="component">An instance of type T</param>
        /// <param name="property">Index of the property requested</param>
        /// <returns>
        /// the value of the property
        /// </returns>
        public object GetPropertyValue(object component, int property)
        {
            var propInfo = _mappedProperties[property].Key;
            return propInfo.GetValue(component, null);
        }

        /// <summary>
        /// Set the value of a property
        /// </summary>
        /// <param name="component">an instance of class mapped by this "type"</param>
        /// <param name="property"></param>
        /// <param name="value">the value to set</param>
        public void SetPropertyValue(object component, int property, object value)
        {
            if (!IsMutable)
                throw new InvalidOperationException(string.Format("{0} is an immutable type.  SetPropertyValue is not supported."));

            var propInfo = _mappedProperties[property].Key;
            propInfo.SetValue(component, value, null);
        }

        bool ICompositeUserType.Equals(object x, object y)
        {
            if (x == null || y == null)
                return false;

            if (ReferenceEquals(x, y) || x == y)
                return true;

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// Retrieves an instance of the mapped class from the database.
        /// Must be overridden if the inheriting class needs to handle nulls.
        /// </summary>
        /// <param name="dr">An IDataReader</param>
        /// <param name="names">Column names</param>
        /// <param name="session">An ISessionImplementor</param>
        /// <param name="owner">The entity that will have it's values populated</param>
        /// <returns>An instance of Type T</returns>
        public virtual object NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            if (dr == null)
                return null;

            var values = new object[names.Length];
            for (var i = 0; i < names.Length; i++)
            {
                var mappedProperty = _mappedProperties[i];
                values[i] = mappedProperty.Value.Get(mappedProperty.Key, dr, names[i], session, owner);
            }
            return CreateInstance(values);
        }

        /// <summary>
        /// Sets the values of the instance for persisting to the database but handles where not all properties should be settable (e.g. calculated)
        /// </summary>
        /// <param name="cmd">An instance of IDbCommand</param>
        /// <param name="value">The instance of the class</param>
        /// <param name="index">Position to start the mapping of properties from</param>
        /// <param name="settable">Array of booleans indicating which properties are settable</param>
        /// <param name="session">An instance of ISessionImplementor</param>
        public void NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (value == null)
                return;

            var propIndex = index;
            foreach (var mappedProperty in _mappedProperties)
            {
                mappedProperty.Value.Set(mappedProperty.Key, value, cmd, propIndex, session);
                propIndex++;
            }
        }

        /// <summary>
        /// The type of object that is mapped to the database.
        /// </summary>
        public Type ReturnedClass
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// Gets names of properties that are mapped
        /// </summary>
        public string[] PropertyNames
        {
            get { return _mappedProperties.Select(p => p.Key.Name).ToArray(); }
        }

        /// <summary>
        /// Gets nhibernate property types that are mapped
        /// </summary>
        public virtual IType[] PropertyTypes
        {
            get { return _mappedProperties.Select(p => NHibernateUtil.GuessType(p.Key.PropertyType)).ToArray(); }
        }

        /// <summary>
        /// Gets PropertyInfo of each mapped property
        /// </summary>
        public PropertyInfo[] Properties
        {
            get { return _mappedProperties.Select(p => p.Key).ToArray(); }
        }

        /// <summary>
        /// Returns whether this object is mutable or not
        /// </summary>
        public abstract bool IsMutable
        {
            get;
        }

        /// <summary>
        /// Return a deep copy of the persisted entity.
        /// </summary>
        /// <param name="value">Instance to be copied</param>
        /// <returns>
        /// </returns>
        public object DeepCopy(object value)
        {
            return PerformDeepCopy((T)value);
        }

        /// <summary>
        /// Used for caching purposes in NHibernate.  Simply performs a DeepCopy.
        /// More complex behaviour has to be implemented by the inheriting class.
        /// </summary>
        /// <param name="value">Instance to be cached</param>
        /// <param name="session">ISessionImplementor instance</param>
        /// <returns>
        /// </returns>
        public virtual object Disassemble(object value, ISessionImplementor session)
        {
            return DeepCopy(value);
        }

        /// <summary>
        /// Rehydrate an instance from the cached version.  Simply performs a DeepCopy.
        /// More complex behaviour has to be implemented by the inheriting class.
        /// </summary>
        /// <param name="cached">Cached version of the object</param>
        /// <param name="session">ISessionImplementor instance</param>
        /// <param name="owner">Instance to be rehydrated</param>
        /// <returns>
        /// </returns>
        public virtual object Assemble(object cached, ISessionImplementor session, object owner)
        {
            return DeepCopy(cached);
        }

        /// <summary>
        /// Used when merging
        /// </summary>
        public virtual object Replace(object original, object target, ISessionImplementor session, object owner)
        {
            return !this.IsMutable ? original : this.DeepCopy(original);
        }
    }
}