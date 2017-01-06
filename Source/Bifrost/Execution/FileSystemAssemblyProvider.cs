/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that is capable of
    /// discovering assembly files from the directory in which the application resides
    /// </summary>
    public class FileSystemAssemblyProvider : ICanProvideAssemblies
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FileSystemAssemblyProvider"/>
        /// </summary>
        /// <param name="fileSystem"></param>
        public FileSystemAssemblyProvider(IFileSystem fileSystem)
        {
            var codeBase = typeof(FileSystemAssemblyProvider).GetTypeInfo().Assembly.CodeBase;
            var uri = new Uri(codeBase);

            var assemblyFileInfo = new FileInfo(uri.LocalPath);

            var assemblyFiles = fileSystem.GetFilesFrom(assemblyFileInfo.Directory.ToString(), "*.dll").ToList();
            assemblyFiles.AddRange(fileSystem.GetFilesFrom(assemblyFileInfo.Directory.ToString(), "*.exe"));

            AvailableAssemblies = assemblyFiles.Select(file => new AssemblyInfo(Path.GetFileNameWithoutExtension(file.FullName), file.FullName));
        }

#pragma warning disable 1591 // Xml Comments
        public event AssemblyAdded AssemblyAdded = (a) => { };

        public IEnumerable<AssemblyInfo> AvailableAssemblies { get; private set; }

        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyInfo.Name));
            AssemblyAdded(assembly);
            return assembly;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
