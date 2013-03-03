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
using Bifrost.Commands;
using System.Collections.Generic;

namespace Bifrost.Statistics
{
    /// <summary>
    /// A statistics plugin interface
    /// </summary>
    public interface IStatisticsPlugin
    {
        /// <summary>
        /// The context that this plugin is working in
        /// </summary>
        string Context { get; }

        /// <summary>
        /// A list of categories that this plugin applies to a statistic
        /// </summary>
        ICollection<string> Categories { get; }

        /// <summary>
        /// Should the statistics data for a handled command by effected by the plugin
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns>True if the plugin effected statistics</returns>
        bool WasHandled(Command command);

        /// <summary>
        /// Should the statistics data for a command that had an exception be effected by the plugin
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns>True if the plugin effected statistics</returns>
        bool HadException(Command command);

        /// <summary>
        /// Should the statistics data for a command that had a validation error be effected by the plugin
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns>True if the plugin effected statistics</returns>
        bool HadValidationError(Command command);

        /// <summary>
        /// Should the statistics data for a command that did not pass security be effected by the plugin
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns>True if the plugin effected statistics</returns>
        bool DidNotPassSecurity(Command command);
    }
}
