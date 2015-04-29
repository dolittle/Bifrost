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
using System.Data.Entity.ModelConfiguration;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Represents a "null" implementation of an <see cref="EntityTypeConfiguration{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullEntityTypeConfiguration<T> : EntityTypeConfiguration<T>
        where T:class
    {
    }
}
