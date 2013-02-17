#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
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
using Bifrost.Execution;
using Castle.DynamicProxy;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents an <see cref="IInterceptor"/> for intercepting properties in a class implementing <see cref="IHaveResources"/>
    /// </summary>
    [Singleton]
    public class ResourceInterceptor : IInterceptor
    {
    	readonly IResourceResolver _resolver;

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceInterceptor"/>
        /// </summary>
        /// <param name="resolver"></param>
    	public ResourceInterceptor(IResourceResolver resolver)
		{
			_resolver = resolver;
		}

#pragma warning disable 1591 // Xml Comments
        public virtual void Intercept(IInvocation invocation)
    	{
            var resourceName = string.Format("{0}.{1}", invocation.Method.DeclaringType.Name, invocation.Method.Name.Replace("get_", string.Empty));

            var value = _resolver.Resolve(resourceName);
            if( !string.IsNullOrEmpty(value) )
                invocation.ReturnValue = value;
            else
                invocation.Proceed();
        }
#pragma warning restore 1591 // Xml Comments

    }
}