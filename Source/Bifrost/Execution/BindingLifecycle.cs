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
namespace Bifrost.Execution
{
	/// <summary>
	/// Scope for activation
	/// </summary>
	public enum BindingLifecycle
	{
		/// <summary>
		/// Default - none
		/// </summary>
		None = 0,

		/// <summary>
		/// Scoped as a singleton within the Ninject kernel
		/// </summary>
		Singleton,

		/// <summary>
		/// Scoped as per request - tied into the current WebRequest
		/// </summary>
		Request,

		/// <summary>
		/// Scoped to null
		/// </summary>
		Transient,

		/// <summary>
		/// Scoped to per thread 
		/// </summary>
		Thread
	}
}