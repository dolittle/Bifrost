using System;
using Bifrost.Commands;

namespace Bifrost.SignalR.Commands
{
    /// <summary>
    /// Represents a descriptor for a <see cref="ICommand"/>
    /// </summary>
    public class CommandDescriptor
    {
        /// <summary>
        /// Gets or sets the Id of the command
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of command
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Namespace of command
        /// </summary>
        public string GeneratedFrom { get; set; }

        /// <summary>
        /// Command content
        /// </summary>
        public string Command { get; set; }
    }

}