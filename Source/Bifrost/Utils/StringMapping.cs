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
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
namespace Bifrost.Utils
{
    /// <summary>
    /// Represents a mapping for a string
    /// </summary>
    public class StringMapping : IStringMapping
    {
        const string PlaceholderExpression = "\\{[a-zA-Z]+\\}";
        const string WildcardExpression = "\\*{2}[//||\\.]";
        const string CombinedExpression = "(" + PlaceholderExpression + ")*(" + WildcardExpression + ")*";

		//static Regex PlaceholderRegex = new Regex(PlaceholderExpression);
        static Regex WildcardRegex = new Regex(WildcardExpression);
        static Regex CombinedRegex = new Regex(CombinedExpression);

        MatchCollection _mappedFormatWildcardMatch;
        Regex _formatRegex;
        string[] _components;

        /// <summary>
        /// Initializes a new instance of <see cref="StringMapping"/>
        /// </summary>
        /// <param name="format">Originating format</param>
        /// <param name="mappedFormat">Mapped format</param>
        public StringMapping(string format, string mappedFormat)
        {
            Format = format;
            MappedFormat = mappedFormat;

            Prepare(format);
        }

        void Prepare(string format)
        {
            var components = new List<string>();
            var resolveExpression = CombinedRegex.Replace(format, m =>
            {
                if (string.IsNullOrEmpty(m.Value)) return string.Empty;
                components.Add(m.Value);
                if (m.Value.StartsWith("**")) return "([\\w.//]*)";
                return "([\\w.]*)";
            });
            _components = components.ToArray();

            _mappedFormatWildcardMatch = WildcardRegex.Matches(MappedFormat);
            _formatRegex = new Regex(resolveExpression);
        }

#pragma warning disable 1591 // Xml Comments
        public string Format { get; private set; }
        public string MappedFormat { get; private set; }

        public bool Matches(string input)
        {
            var match = _formatRegex.Match(input);
            return match.Success;
        }

        public dynamic GetValues(string input)
        {
            var values = new ExpandoObject();

            var match = _formatRegex.Match(input);
            for (var componentIndex = 0; componentIndex < _components.Length; componentIndex++)
            {
                var component = _components[componentIndex];
                component = component.Substring(1, component.Length - 2);
                if (!component.StartsWith("**"))
                    ((IDictionary<string,object>)values)[component] = match.Groups[componentIndex + 2].Value;
            }
            return values;
        }

        public string Resolve(string input)
        {
            var matches = _formatRegex.Match(input).Groups;
            var result = MappedFormat;
            var wildcardOffset = 0;

            for( var componentIndex=0; componentIndex<_components.Length; componentIndex++ ) 
            {
                var component = _components[componentIndex];
                var value = matches[componentIndex+1].Value;
                if (component.StartsWith("**"))
                {
                    var wildcard = _mappedFormatWildcardMatch[wildcardOffset];
                    value = value.Replace(component[2], wildcard.Value[2]);
                    result = result.Replace(wildcard.Value, value);
                    wildcardOffset++;
                }
                else
                    result = result.Replace(component, value);
            }

            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
