#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a base class for any <see cref="ISecurityActionBuilder">security action builders</see>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ISecurityAction"/> it is building</typeparam>
    public class SecurityActionBuilder<T> : ISecurityActionBuilder<T> where T:ISecurityAction
    {
#pragma warning disable 1591 // Xml Comments
        protected SecurityActionBuilder(T action)
        {
            Action = action;
        }

        public T Action { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
