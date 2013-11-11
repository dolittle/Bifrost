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
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="IProblems"/>
    /// </summary>
    public class Problems : IProblems
    {
        List<Problem> _problems = new List<Problem>();

#pragma warning disable 1591 // Xml Comments
        public void Report(ProblemType type, object data)
        {
            _problems.Add(new Problem
            {
                Type = type,
                Data = data
            });
        }

        public bool Any { get { return _problems.Count > 0; } }
 
        public IEnumerator<Problem> GetEnumerator()
        {
            return _problems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _problems.GetEnumerator();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
