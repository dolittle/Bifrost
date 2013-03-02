﻿using Bifrost.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
