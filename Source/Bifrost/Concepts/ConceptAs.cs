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
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Expresses a Concept as a another type, usually a primitive such as Guid, Int or String
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConceptAs<T> : IAmValidatable ,IEquatable<ConceptAs<T>> where T : IEquatable<T>
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
            if (a == null && b == null)
                return true;

            if (a == null ^ b == null)
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

        public bool IsEmpty()
        {
            if (!(Value != null))
                return true;

            var value = Value as string;

            if (value != null)
                return value == string.Empty;

            return Value.Equals(default(T));
        }
#pragma warning restore 1591 // Xml Comments
    }
}