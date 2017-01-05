/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IFileSystem"/>
    /// </summary>
    public class FileSystem : IFileSystem
    {
#pragma warning disable 1591 // Xml Comments
        public IEnumerable<FileInfo> GetFilesFrom(string path, string searchPattern)
        {
            return new DirectoryInfo(path).GetFiles(searchPattern);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
