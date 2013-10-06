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
using Bifrost.CodeGeneration.JavaScript;

namespace Bifrost.Web.Proxies
{
    /// <summary>
    /// Defines the extender for properties on commands - a visitor that can take part of the proxy generation of properties on commands
    /// </summary>
    public interface ICanExtendCommandProperty
    {
        /// <summary>
        /// Extend a given property
        /// </summary>
        /// <param name="commandType">Type of command property belongs to</param>
        /// <param name="propertyName">Name of the property that can be extended</param>
        /// <param name="observable">The observable that can be extended</param>
        void Extend(Type commandType, string propertyName, Observable observable);
    }
}
