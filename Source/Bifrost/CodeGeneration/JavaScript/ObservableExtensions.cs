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
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with observables
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        /// Set the default value for the observable
        /// </summary>
        /// <param name="observable"><see cref="Observable"/> to set default value for</param>
        /// <param name="defaultValue">Default value to set</param>
        /// <returns>The <see cref="Observable"/> to build on</returns>
        public static Observable WithDefaultValue(this Observable observable, object defaultValue)
        {
            observable.Parameters = new Literal[] { new Literal(defaultValue) };
            return observable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Observable ExtendWith(this Observable observable, string name, string options)
        {
            observable.AddExtension(name, options);
            return observable;
        }
    }
}
