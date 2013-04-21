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
using System;
using Microsoft.Practices.Unity;

namespace Bifrost.Unity
{
    // Based on : http://xhalent.wordpress.com/2011/02/02/resolving-instances-using-delegates-in-unity/
    public class DelegateLifetimeManager : LifetimeManager
    {
        private LifetimeManager _baseManager;
        private Func<object> _resolveDelegate = null;

        public DelegateLifetimeManager(
            Func<object> sourceFunc,
            LifetimeManager baseManager = null)
        {
            this._resolveDelegate = sourceFunc;
            this._baseManager = baseManager;
        }

        public override object GetValue()
        {
            object result = _baseManager.GetValue();
            if (result == null)
            {
                result = _resolveDelegate();

                if (_baseManager != null)
                    _baseManager.SetValue(result);
            }

            return result;
        }

        public override void RemoveValue()
        {
            if (_baseManager != null)
                _baseManager.RemoveValue();
        }

        public override void SetValue(object newValue)
        {
            if (_baseManager != null)
                _baseManager.SetValue(newValue);
        }
    }
}
