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

using Bifrost.Execution;
using Bifrost.Validation;
using Bifrost.Validation.MetaData;
using FluentValidation;
using Bifrost.Commands;

namespace Bifrost.Web.Validation
{
    public class ValidationService
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;
        readonly IValidationMetaDataGenerator _validationMetaDataGenerator;
        readonly ITypeDiscoverer _discoverer;

        public ValidationService(
            ICommandValidatorProvider commandValidatorProvider, 
            IValidationMetaDataGenerator validationMetaDataGenerator,
            ITypeDiscoverer discoverer)
        {
            _commandValidatorProvider = commandValidatorProvider;
            _validationMetaDataGenerator = validationMetaDataGenerator;
            _discoverer = discoverer;
        }

        public ValidationMetaData GetForCommand(string name)
        {
            var commandType = _discoverer.GetCommandTypeByName(name);

            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(commandType) as IValidator;
            if (inputValidator != null)
            {
                var metaData = _validationMetaDataGenerator.GenerateFrom(inputValidator);
                return metaData;
            }
            return new ValidationMetaData();
		}
    }
}

