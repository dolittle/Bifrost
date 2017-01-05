/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.Concepts
{
    /// <summary>
    /// A base class for providing value object equality semantics.  A Value Object does not have an identity and its value comes from its properties.
    /// </summary>
    /// <typeparam name="T">The specific type to provide value object equality semantics to.</typeparam>
    public abstract class Value<T> : IAmValidatable, IEquatable<T> where T : Value<T>
    {
        static IList<FieldInfo> _fields { get; set; }

        static Value()
        {
            _fields = new List<FieldInfo>();
        }

        /// <summary>
        /// Checks for Equality between this instance and the obj
        /// </summary>
        /// <param name="obj">An istance of an object to check equality with</param>
        /// <returns>True if equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        /// <summary>
        /// Gets a Hash Code to identify this instance
        /// </summary>
        /// <returns>Hashcode value</returns>
        public override int GetHashCode()
        {
            var fields = GetFields().Select(field => field.GetValue(this)).Where(value => value != null).ToList();
            fields.Add(GetType());
            return HashCodeHelper.Generate(fields.ToArray());
        }

        /// <summary>
        /// Checks for Equality between this instance and the Other
        /// </summary>
        /// <param name="other">Another instance of type T to check equality with</param>
        /// <returns>True if equal, false otherwise</returns>
        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType)
                return false;

            var fields = GetFields();

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Equates two objects to check that they are equal
        /// </summary>
        /// <param name="x">First Value</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are equal, false is they are not</returns>
        public static bool operator ==(Value<T> x, Value<T> y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        /// <summary>
        /// Equates two objects to check that they are not equal
        /// </summary>
        /// <param name="x">First Value</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are not equal, false is they are</returns>
        public static bool operator !=(Value<T> x, Value<T> y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Converts this Value into a string representation.
        /// </summary>
        /// <returns>A string containing each property name and its corresponding value</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{[Type: " + GetType() + "]");
            foreach (var field in GetFields())
            {
                sb.AppendFormat(@"{{ {0} : {1} }}", RemoveBackingAutoBackingFieldPropertyName(field.Name), field.GetValue(this) ?? "[NULL]" );
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        IEnumerable<FieldInfo> GetFields()
        {
            if (!_fields.Any())
                 _fields = new List<FieldInfo>(BuildFieldCollection());
            return _fields;
        }

        IEnumerable<FieldInfo> BuildFieldCollection()
        {
            var t = typeof(T);
            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                var typeInfo = t.GetTypeInfo();
                
                fields.AddRange(typeInfo.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance));
                var fieldInfoCache = typeInfo.GetField("_fields");
                fields.Remove(fieldInfoCache);
                t = typeInfo.BaseType;
            }
            return fields;
        }

        string RemoveBackingAutoBackingFieldPropertyName(string fieldName)
        {
            var field = fieldName.TrimStart('<');
            return field.Replace(@">k__BackingField", String.Empty);
        }
    }
}