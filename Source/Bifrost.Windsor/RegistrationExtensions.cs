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
using Bifrost.Execution;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Core;

namespace Bifrost.Windsor
{
	public static class RegistrationExtensions
	{
		public static ComponentRegistration<T> WithLifecycle<T>(this ComponentRegistration<T> registration, BindingLifecycle lifecycle)
		{
            switch (lifecycle)
            {
                case BindingLifecycle.Singleton: 
                    return registration.LifeStyle.Is(LifestyleType.Singleton);
				
                case BindingLifecycle.Thread : 
					return registration.LifeStyle.Is(LifestyleType.Thread);

                case BindingLifecycle.Transient:
                    return registration.LifeStyle.Is(LifestyleType.Transient);
				
				case BindingLifecycle.Request:
					return registration.LifeStyle.Is(LifestyleType.PerWebRequest);
            }
			 
			return registration;
		}
	}
}
