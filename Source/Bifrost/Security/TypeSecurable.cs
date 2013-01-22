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
    /// Represents a <see cref="Securable"/> that applies to a specific <see cref="System.Type"/>
    /// </summary>
    public class TypeSecurable : Securable
    {
        /// <summary>
        /// Initializes an instance of <see cref="TypeSecurable"/>
        /// </summary>
        /// <param name="type"><see cref="System.Type"/> to secure</param>
        public TypeSecurable(Type type)
        {
            Type = type;
        }


        /// <summary>
        /// Gets the type that is secured
        /// </summary>
        public Type Type { get; private set; }

        public override bool CanAuthorize(object actionToAuthorize)
        {
            return actionToAuthorize!= null && Type == actionToAuthorize.GetType();
        }
    }
}
