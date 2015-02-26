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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Bifrost.Execution
{
	/// <summary>
	/// Represents a comparer for comparing assemblies, typically used in Distinct() 
	/// </summary>
	public class AssemblyComparer : IEqualityComparer<_Assembly>
	{
#pragma warning disable 1591 // Xml Comments
		public bool Equals(_Assembly x, _Assembly y)
		{
			return x.FullName == y.FullName;
		}

		public int GetHashCode(_Assembly obj)
		{
			return obj.GetHashCode();
		}
#pragma warning restore 1591 // Xml Comments
	}
}