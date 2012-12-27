using System;
namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a builder for building <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandBuilder<T> where T:ICommand
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="ICommand"/>being built
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the default parameters for the <see cref="ICommand"/>
        /// </summary>
        dynamic Parameters { get; set; }

        /// <summary>
        /// Gets or sets the constructor parameters for the <see cref="ICommand"/>
        /// </summary>
        /// <remarks>
        /// The order of the parameters must match the constructor.
        /// Also; the amount of parameters must match as well
        /// </remarks>
        object[] ConstructorParameters { get; set; }

        /// <summary>
        /// Gets or sets the type to use for building the <see cref="ICommand"/>
        /// </summary>
        /// <remarks>
        /// Default type will bee <see cref="Command"/>
        /// </remarks>
        Type Type { get; set; }

        /// <summary>
        /// Get an instance of the <see cref="ICommand"/>
        /// </summary>
        /// <returns>An instance of the <see cref="ICommand"/></returns>
        T GetInstance();
    }
}
