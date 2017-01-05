/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines functionality for accessing the filesystem
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Get files for a specific path
        /// </summary>
        /// <param name="path">Path to get files from</param>
        /// <param name="searchPattern">Search pattern to use for filtering</param>
        /// <returns><see cref="IEnumerable{FileInfo}">Enumerable of <see cref="FileInfo"/></see></returns>
        IEnumerable<FileInfo> GetFilesFrom(string path, string searchPattern);
    }
}
