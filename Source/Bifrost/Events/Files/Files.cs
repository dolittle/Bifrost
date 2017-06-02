/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Bifrost.Logging;
using Bifrost.Serialization;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IFiles"/>
    /// </summary>
    public class Files : IFiles
    {
        ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Files"/>
        /// </summary>
        /// <param name="logger"></param>
        public Files(ILogger logger)
        {
            _logger = logger;
        }


        /// <inheritdoc/>
        public void WriteString(string path, string file, string content)
        {
            MakeSurePathExists(path);
            var fullPath = Path.Combine(path, file);

            _logger.Trace($"Writing string to file '{fullPath}'");
            
            using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
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
            MakeSurePathExists(path);
            var fullPath = Path.Combine(path, file);

            _logger.Trace($"Reading string from file '{fullPath}'");

            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.None))
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
            MakeSurePathExists(path);
            var fullPath = Path.Combine(path, file);
            return File.Exists(fullPath);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetFilesIn(string path, string searchPattern)
        {
            MakeSurePathExists(path);
            if( searchPattern == null ) return Directory.GetFiles(path);
            return Directory.GetFiles(path, searchPattern);
        }


        void MakeSurePathExists(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
