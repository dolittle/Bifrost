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

namespace Bifrost.Validation
{
    /// <summary>
    /// The exception that is thrown if a value coming in is of the wrong type from what is expected in a rule
    /// </summary>
    public class ValueTypeMismatch : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValueTypeMismatch"/>
        /// </summary>
        /// <param name="expected">Expected type for value</param>
        /// <param name="actual">Actual type for value</param>
        public ValueTypeMismatch(Type expected, Type actual) : base("Expected '"+expected.Name+"' but got '"+actual.Name+"'")
        {

        }
    }
}
