/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a thread safe context for the current callpath
    /// </summary>
    public interface ICallContext
    {
        /// <summary>
        /// Check if data exists for a given key
        /// </summary>
        /// <param name="key">Key to check if data exists for</param>
        /// <returns>True if exists, false if not</returns>
        bool HasData(string key);

        /// <summary>
        /// Get data with a specific key
        /// </summary>
        /// <typeparam name="T">Type of data you're getting</typeparam>
        /// <param name="key">Key representing the data</param>
        /// <returns>An instance of the data, if any</returns>
        T GetData<T>(string key);

        /// <summary>
        /// Set data for a given key
        /// </summary>
        /// <param name="key">Key to set for</param>
        /// <param name="data">Data to set</param>
        void SetData(string key, object data);
    }
}
