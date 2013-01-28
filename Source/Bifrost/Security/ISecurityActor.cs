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
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines the security object that defines the security
    /// </summary>
    public interface ISecurityActor
    {
        /// <summary>
        /// Authorizes an instance of an action
        /// </summary>
        /// <param name="actionToAuthorize">An instance of the action to authorize</param>
        /// <returns>A <see cref="AuthorizeActorResult"/> which encapsulates the result of the authorization attempt of this instance of an action</returns>
        AuthorizeActorResult IsAuthorized(object actionToAuthorize);

        /// <summary>
        /// Gets a description of the SecurityActor.
        /// </summary>
        string Description { get; }
    }
}
