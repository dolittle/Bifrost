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
using System;
using System.Windows.Markup;
using Bifrost.Configuration;
using Bifrost.Dynamic;

namespace Bifrost.ViewModels
{
    [MarkupExtensionReturnType(typeof(object))]
    public class ViewModelExtension : MarkupExtension
    {
        Type _type;

        public ViewModelExtension(Type type)
        {
            _type = type;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {

                var instance = Configure.Instance.Container.Get(_type);
                return instance;
            }
            catch
            {
                return new BindableExpandoObject();
            }
        }
    }
}
