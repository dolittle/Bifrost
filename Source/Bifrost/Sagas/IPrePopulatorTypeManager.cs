using System;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines a manager for dealing with <see cref="IPrePopulate">IPrePopulates</see> types
    /// </summary>
    public interface IPrePopulatorTypeManager
    {
        /// <summary>
        /// Gets the type of the IPrePopulate given the saga and command names (combination must be unique)
        /// </summary>
        /// <param name="sagaName">The name of the <see cref="ISaga">saga</see></param>
        /// <param name="commandName">The name of the <see cref="ICommand"/></param>
        /// <returns></returns>
        Type GetFromNames(string sagaName, string commandName);
    }
}