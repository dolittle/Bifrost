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
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.Web.Commands
{
    public class CommandTypeConverterPropertyExtender : ICanExtendCommandProperty
    {
        public void Extend(Type commandType, string propertyName, Observable observable)
        {
            var property = commandType.GetProperty(propertyName.ToPascalCase());
            if (property != null)
            {
                var type = property.PropertyType;
                if (type.IsConcept()) type = type.GetConceptValueType();

                var clientType = string.Empty;

                if (type == typeof(byte) ||
                    type == typeof(Int16) ||
                    type == typeof(Int32) ||
                    type == typeof(Int64) ||
                    type == typeof(UInt16) ||
                    type == typeof(UInt32) ||
                    type == typeof(UInt64) ||
                    type == typeof(float) ||
                    type == typeof(double) ||
                    type == typeof(decimal) )
                {
                    clientType = "Number";
                }
                else if (type == typeof(DateTime))
                {
                    clientType = "Date";
                }

                if (!string.IsNullOrEmpty(clientType)) observable.ExtendWith("typeConverter", string.Format("\"{0}\"",clientType));
            }
        }
    }
}