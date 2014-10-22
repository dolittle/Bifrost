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
using Bifrost.Execution;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents an implementation of <see cref="IValidationMetaData"/>
    /// </summary>
    public class ValidationMetaData : IValidationMetaData
    {
        IInstancesOf<ICanGenerateValidationMetaData> _generators;

        /// <summary>
        /// Initializes an instance of <see cref="ValidationMetaData"/>
        /// </summary>
        public ValidationMetaData(IInstancesOf<ICanGenerateValidationMetaData> generators)
        {
            _generators = generators;
        }

#pragma warning disable 1591 // Xml Comments
        public TypeMetaData GetMetaDataFor(Type typeForValidation)
        {
            var typeMetaData = new TypeMetaData();

            foreach (var generator in _generators)
            {
                var metaData = generator.GenerateFor(typeForValidation);

                foreach (var property in metaData.Properties.Keys)
                {
                    foreach( var ruleSet in metaData.Properties[property].Keys ) 
                    {
                        typeMetaData[property][ruleSet] = metaData.Properties[property][ruleSet];
                    }
                }                
            }

            return typeMetaData;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
