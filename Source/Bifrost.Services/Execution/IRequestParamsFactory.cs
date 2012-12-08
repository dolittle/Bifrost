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
using System.Web;

namespace Bifrost.Services.Execution
{
    /// <summary>
    /// Defines a factory for building a <see cref="RequestParams">Request Parameters collection </see>
    /// </summary>
    public interface IRequestParamsFactory
    {
        /// <summary>
        /// Builds an instance of <see cref="RequestParams">RequestParams</see> that encapsulates request parameters
        /// </summary>
        /// <param name="request">An HttpRequestBase instance</param>
        /// <returns></returns>
        RequestParams BuildParamsCollectionFrom(HttpRequestBase request);
    }
}