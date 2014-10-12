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
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.DocumentDB.Entities
{
    /// <summary>
    /// Implements the <see cref="IEntityContextConfiguration"/> specific for the DocumentDB support
    /// </summary>
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        /// <summary>
        /// Gets or sets the url endpoint for the database server
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the database id 
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the authorization key
        /// </summary>
        public string AuthorizationKey { get; set; }


#pragma warning disable 1591 // Xml Comments
        public Type EntityContextType { get { return typeof(EntityContext<>); } }

        public IEntityContextConnection Connection { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
