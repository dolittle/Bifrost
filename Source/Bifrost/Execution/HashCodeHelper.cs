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
using System.Linq;

namespace Bifrost.Execution
{
    /// <summary>
    /// Provides useful methods for dealing with HashCodes
    /// </summary>
    public static class HashCodeHelper
    {
        /// <summary>
        /// Encapsulates an algorithm for generating a hashcode from a series of parameters
        /// </summary>
        /// <param name="parameters">Properties to generate the HashCode from.</param>
        /// <returns>Hash Code</returns>
	    public static int Generate(params object[] parameters)
	    {
            //http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
		    unchecked
		    {
		        return parameters.Where(param => param != null)
                            .Aggregate(17, (current, param) => current*29 + param.GetHashCode());
		    }
	    }
    }
}