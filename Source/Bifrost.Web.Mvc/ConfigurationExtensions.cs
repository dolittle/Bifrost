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
using System.Web.Mvc;
using Bifrost.Validation;
using Bifrost.Web.Mvc;
using FluentValidation.Mvc;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {

        public static IConfigure ConfigureDependencyResolver(this IConfigure configure)
        {
            var dependencyResolver = new ContainerDependencyResolver(configure.Container);
            DependencyResolver.SetResolver(dependencyResolver);
            return configure;
        }

        public static IConfigure UsingFluentValidation(this IConfigure configure)
        {
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            var factory = configure.Container.Get<CommandValidatorFactory>();
            FluentValidationModelValidatorProvider.Configure(p =>
            {
                p.ValidatorFactory = factory;
                p.AddImplicitRequiredValidator = false;
            });

            var validatorFactory = configure.Container.Get<DefaultValidatorFactory>();
            var validatorProvider = new FluentValidationModelValidatorProvider(validatorFactory);
            validatorProvider.AddImplicitRequiredValidator = false;
            ModelValidatorProviders.Providers.Add(validatorProvider);

            return configure;
        }
    }
}
