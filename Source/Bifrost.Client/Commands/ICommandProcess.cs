using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandProcess
    {
        /// <summary>
        /// Add a <see cref="CommandSucceeded"/> callback that gets called 
        /// when the command is successfully handled
        /// </summary>
        /// <param name="callback"><see cref="CommandSucceeded">Callback</see> to add</param>
        void Succeeded(CommandSucceeded callback);

        /// <summary>
        /// Add a <see cref="CommandFailed"/> callback that gets called 
        /// when the command fails during handling
        /// </summary>
        /// <param name="callback"><see cref="CommandFailed">Callback</see> to add</param>
        void Failed(CommandFailed callback);

        /// <summary>
        /// Add a <see cref="CommandHandled"/> callback that gets called
        /// when the command is handled disregarding wether or not it was successful
        /// </summary>
        /// <param name="callback"><see cref="CommandHandled">Callback</see> to add</param>
        void Handled(CommandHandled callback);

    }
}
