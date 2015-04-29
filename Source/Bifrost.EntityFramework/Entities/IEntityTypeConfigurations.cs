#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Defines a system that knows about all <see cref="EntityTypeConfiguration{T}"/> implementations
    /// </summary>
    public interface IEntityTypeConfigurations 
    {
        /// <summary>
        /// Get configuration for a specific type
        /// </summary>
        /// <typeparam name="T">Type to get for</typeparam>
        /// <returns><see cref="EntityTypeConfiguration{T}"/> instance - if non is found, returns a <see cref="NullEntityTypeConfiguration{T}"/></returns>
        EntityTypeConfiguration<T> GetFor<T>() where T : class;
    }
}
