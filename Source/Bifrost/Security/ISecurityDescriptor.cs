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
using System;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a security descriptor
    /// </summary>
    public interface ISecurityDescriptor
    {
        /// <summary>
        /// Define security for a specific namespace
        /// </summary>
        /// <param name="namespace">Namespace to define for</param>
        /// <returns>The <see cref="ISecurable"/> representing the namespace</returns>
        ISecurable ForNamespace(string @namespace);

        /// <summary>
        /// Define security for the namespace of a given type
        /// </summary>
        /// <typeparam name="T">Type that represents the namespace</typeparam>
        /// <returns>The <see cref="ISecurable"/> representing the namespace</returns>
        ISecurable ForNamespaceOf<T>();

        /// <summary>
        /// Define security for a specific type
        /// </summary>
        /// <typeparam name="T">Type to define for</typeparam>
        /// <returns>The <see cref="ISecurable"/> representing the type</returns>
        ISecurable For<T>();

        /// <summary>
        /// Define security for a specific type
        /// </summary>
        /// <param name="type">Type to define for</param>
        /// <returns>The <see cref="ISecurable"/> representing the type</returns>
        ISecurable For(Type type);
    }
}
