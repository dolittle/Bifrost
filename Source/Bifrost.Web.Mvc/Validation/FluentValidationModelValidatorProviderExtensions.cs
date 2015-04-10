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
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Validation
{
    public static class FluentValidationModelValidatorProviderExtensions
    {
        public static void AddPropertyValidators(this FluentValidationModelValidatorProvider provider)
        {
            provider.Add(typeof(GreaterThanValidator),
                (metadata, context, rule, validator) => new GreaterThanPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(GreaterThanOrEqualValidator),
                (metadata, context, rule, validator) => new GreaterThanOrEqualPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(LessThanValidator),
                (metadata, context, rule, validator) => new LessThanPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(LessThanOrEqualValidator),
                (metadata, context, rule, validator) => new LessThanPropertyValidator(metadata, context, rule, validator));
        }
    }
}
