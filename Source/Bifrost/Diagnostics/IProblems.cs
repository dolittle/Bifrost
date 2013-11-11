﻿#region License
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
using System.Collections.Generic;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a collection of <see cref="Problem">problems</see>
    /// </summary>
    public interface IProblems : IEnumerable<Problem>
    {
        /// <summary>
        /// Report a <see cref="Problem"/>
        /// </summary>
        /// <param name="type"><see cref="ProblemType">Type of problem</see> to report</param>
        /// <param name="data">Data associated with the problem</param>
        void Report(ProblemType type, object data);

        /// <summary>
        /// Gets wether or not it has any problems
        /// </summary>
        bool Any { get; }
    }
}
