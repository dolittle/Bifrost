using Bifrost.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost
{
    /// <summary>
    /// Command Statistics
    /// </summary>
    public interface ICommandStatistics
    {
        /// <summary>
        /// Adds a command that was handled to statistics
        /// </summary>
        /// <param name="command">The command</param>
        void WasHandled(Command command);

        /// <summary>
        /// Adds a command that had an exception to statistics
        /// </summary>
        /// <param name="command"></param>
        void HadException(Command command);

        /// <summary>
        /// Add a command that had a validation error to statistics
        /// </summary>
        /// <param name="command">The command</param>
        void HadValidationError(Command command);
        
        /// <summary>
        /// Adds a command that did not pass security to statistics
        /// </summary>
        /// <param name="command"></param>
        void DidNotPassSecurity(Command command);
    }
}
