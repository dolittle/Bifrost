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
    /// Provides a set of extensions for building rules
    /// </summary>
    public static class SecurityRuleBuilderExtensions
    {
        /// <summary>
        /// Specifies in a specific namespace
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public static ISecurableBuilder InNamespace(this ISecurityRuleBuilder builder, string @namespace)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityRuleBuilder"></param>
        /// <returns></returns>
        public static ISecurityObject User(this ISecurableBuilder securityRuleBuilder)
        {
            return null;
        }
    }
}
