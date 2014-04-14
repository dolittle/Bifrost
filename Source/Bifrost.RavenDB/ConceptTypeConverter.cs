#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using Raven.Client.Converters;
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.RavenDB
{
    public class ConceptTypeConverter<T,TValue> : ITypeConverter where T : ConceptAs<TValue>, new() where TValue : IEquatable<TValue>
    {
        public bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof (T);
        }

        public string ConvertFrom(string tag, object value, bool allowNull)
        {
            if (value == null && allowNull)
                return null;

            var stringValue = value == null ? string.Empty : value.ToString();

            return string.Concat(tag,stringValue);
        }

        public object ConvertTo(string value)
        {
            var conceptValue = value.ParseTo(typeof(TValue));
            return new T {Value = (TValue)conceptValue};
        }
    }
}
