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

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="Securable"/> that applies to a specific namespace
    /// </summary>
    public class NamespaceSecurable : Securable
    {
        const string NAMESPACE = "InNamespace_{{{0}}}";

        /// <summary>
        /// Initializes a new instance of <see cref="NamespaceSecurable"/>
        /// </summary>
        /// <param name="namespace">Namespace to secure</param>
        public NamespaceSecurable(string @namespace) : base(string.Format(NAMESPACE,@namespace))
        {
            Namespace = @namespace;
        }

        /// <summary>
        /// Gets the namespace that is secured
        /// </summary>
        public string Namespace { get; private set; }

#pragma warning disable 1591
        public override bool CanAuthorize(object actionToAuthorize)
        {
            return actionToAuthorize != null && actionToAuthorize.GetType().Namespace.StartsWith(Namespace,
#if(NETFX_CORE)
                StringComparison.Ordinal);
#else
                StringComparison.InvariantCulture);
#endif
        }
#pragma warning restore 1591
    }
}
