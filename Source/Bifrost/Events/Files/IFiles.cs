/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Defines a system for working with files on the filesystem
    /// </summary>
    public interface IFiles
    {
        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <param name="path">Path to check for file in</param>
        /// <param name="file">File to check if exists</param>
        /// <returns>True if it exists, false if not</returns>
        bool Exists(string path, string file);

        /// <summary>
        /// Write a string to the filesystem
        /// </summary>
        /// <param name="path">Path to where to write the file</param>
        /// <param name="file">Name of the file to write</param>
        /// <param name="content">String holding the content to write</param>
        void WriteString(string path, string file, string content);

        /// <summary>
        /// Read a string from a file on the filesystem
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="file">File to read</param>
        /// <returns>String content</returns>
        string ReadString(string path, string file);

        /// <summary>
        /// Get files in a directory based on search pattern, typically extensions
        /// </summary>
        /// <param name="directory">Directory to get files from</param>
        /// <param name="searchPattern">Search pattern e.g. *.dll</param>
        /// <returns><see cref="IEnumerable{T}">Files</see></returns>
        IEnumerable<string> GetFilesIn(string directory, string searchPattern=null);
    }
}
