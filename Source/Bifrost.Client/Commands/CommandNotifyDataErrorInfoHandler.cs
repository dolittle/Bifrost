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
using System.Collections;
using System.ComponentModel;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="INotifyDataErrorInfo"/>
    /// that will handle any invocations from an interceptor
    /// </summary>
    public class CommandNotifyDataErrorInfoHandler : INotifyDataErrorInfo
    {
#pragma warning disable 1591 // Xml Comments
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = (s, e) => { };

        public IEnumerable GetErrors(string propertyName)
        {
            return new[] { "Its just wrong!", "And more stuff is wrong as well!" };
        }

        public bool HasErrors
        {
            get { return true; }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
