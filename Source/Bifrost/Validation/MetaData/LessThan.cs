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
namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the metadata for the LessThan validation rule
    /// </summary>
    public class LessThan : Rule
    {
        /// <summary>
        /// Gets or sets the value that values validated up against must be less than
        /// </summary>
        public object Value { get; set; }
    }
}
