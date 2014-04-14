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

using Bifrost.Execution;

namespace Bifrost.Autofac
{


    public static class ContainerExtensions
    {
        public static global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
            PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>
            (this global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder,
             BindingLifecycle lifeStyle)
        { 
            //no thread lifecycle
            switch (lifeStyle)
            {
                case BindingLifecycle.Request:
                    return builder.InstancePerLifetimeScope();
                case BindingLifecycle.Transient:
                    return builder.InstancePerDependency();
                case BindingLifecycle.Singleton:
                    return builder.SingleInstance();
                default:
                    return builder.InstancePerDependency();
            }
        }
    }
}