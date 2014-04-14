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

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Helps create NHibernate Custom Mappings for Types
    /// </summary>
    /// <typeparam name="T">Type that this user type maps</typeparam>
    public abstract class ImmutableUserType<T> : UserTypeBase<T>
    {
#pragma warning disable 1591 // Xml Comments
        protected override T CreateInstance(object[] propertyValues)
        {
            var instance = Activator.CreateInstance<T>();

            for (var i = 0; i < propertyValues.Length; i++)
            {
                Properties[i].SetValue(instance, propertyValues[i], null);
            }

            return instance;
        }

        public override bool IsMutable
        {
            get { return false; }
        }

        protected override T PerformDeepCopy(T source)
        {
            return source;
        }
    }
#pragma warning restore // Xml Comments
}