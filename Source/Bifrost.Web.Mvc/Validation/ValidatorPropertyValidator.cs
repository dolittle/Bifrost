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
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Validation
{
    public class ValidatorPropertyValidator : FluentValidationPropertyValidator
    {
        public ValidatorPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
            ShouldValidate = false;

            var model = controllerContext.Controller.ViewData.Model;
            if (validator is PropertyValidatorWithDynamicState)
            {
                
                DynamicState = new DynamicState(model, ((PropertyValidatorWithDynamicState)validator).Properties);
            }
            else
            {
                DynamicState = new DynamicState(model);
            }
        }

        public dynamic DynamicState { get; private set; }
    }
}