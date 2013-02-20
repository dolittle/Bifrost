#region License
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
using System;

namespace Bifrost.Oracle.Events
{
    public class EventDto
    {
        public long Id { get; set; }
        public Guid CommandContext { get; set; }
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public Guid EventSourceId { get; set; }
        public string EventSource { get; set; }
        public int Generation { get; set; }
        public string Data { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
        public DateTime Occurred { get; set; }
        public double Version { get; set; }
    }
}