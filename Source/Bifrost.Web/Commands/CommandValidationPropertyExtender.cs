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
using System;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Validation;
using Bifrost.Validation.MetaData;
using Newtonsoft.Json;

namespace Bifrost.Web.Commands
{
    public class CommandValidationPropertyExtender : ICanExtendCommandProperty
    {
        IValidationMetaDataGenerator _validationMetaDataGenerator;

        public CommandValidationPropertyExtender(IValidationMetaDataGenerator validationMetaDataGenerator)
        {
            _validationMetaDataGenerator = validationMetaDataGenerator;
        }

        public void Extend(Type commandType, string propertyName, Observable observable)
        {
            var metaData = _validationMetaDataGenerator.GenerateFor(commandType);
            if (metaData.Properties.ContainsKey(propertyName))
            {
                var options = JsonConvert.SerializeObject(metaData.Properties[propertyName],
                    new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
                observable.ExtendWith("validation", options);
            }
        }
    }
}
