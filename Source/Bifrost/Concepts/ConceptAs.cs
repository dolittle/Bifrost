using System;
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Expresses a Concept as a another type, usually a primitive such as Guid, Int or String
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConceptAs<T> : IEquatable<ConceptAs<T>> where T : IEquatable<T>
    {
        /// <summary>
        /// The underlying primitive value of this concept
        /// </summary>
        public T Value { get; set; }

#pragma warning disable 1591 // Xml Comments
        public override string ToString()
        {
            return Value == null ? default(T).ToString() : Value.ToString();
        }

        public static implicit operator T(ConceptAs<T> value)
        {
            return value == null ? default(T) : value.Value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as ConceptAs<T>);
        }

        public virtual bool Equals(ConceptAs<T> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var t = GetType();
            var otherType = other.GetType();

            return t == otherType && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public static bool operator ==(ConceptAs<T> a, ConceptAs<T> b)
        {
            if ((object)a == null && (object)b == null)
                return true;

            if ((object)a == null ^ (object)b == null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ConceptAs<T> a, ConceptAs<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.Generate(typeof (T), Value);
        }
#pragma warning restore 1591 // Xml Comments
    }
}