/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Bifrost.Serialization;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IFiles"/>
    /// </summary>
    public class Files : IFiles
    {
        /// <inheritdoc/>
        public void WriteString(string path, string file, string content)
        {
            MakeSurePathExists(path);
            var fullPath = Path.Combine(path, file);

            using (var stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                }
            }
        }

        /// <inheritdoc/>
        public string ReadString(string path, string file)
        {
            var fullPath = Path.Combine(path, file);

            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd().Trim();
                    return content;
                }
            }
        }


        /// <inheritdoc/>
        public bool Exists(string path, string file)
        {
            var fullPath = Path.Combine(path, file);
            return File.Exists(fullPath);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetFilesIn(string directory, string searchPattern)
        {
            return Directory.GetFiles(directory, searchPattern);
        }


        void MakeSurePathExists(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
