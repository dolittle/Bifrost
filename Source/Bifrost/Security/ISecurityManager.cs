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
    /// Defines a manager for dealing with security for types and namespaces
    /// </summary>
    public interface ISecurityManager
    {
        /// <summary>
        /// Ask if a <see cref="ISecurable"/> has access
        /// </summary>
        /// <param name="target">Object that is subject of security</param>
        /// <returns>True if allowed, false if not</returns>
        /// <remarks>
        /// <see cref="ISecurable"/> is a concept used when describing security, not an interface
        /// a securable item needs to implement
        /// </remarks>
        bool IsAllowed<T>(object target) where T : ISecurityAction;
    }
}
