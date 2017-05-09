/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents the information about assemblies
    /// </summary>
    public class AssemblyInfo : IEquatable<AssemblyInfo>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyInfo"/>
        /// </summary>
        /// <param name="name">Name of the assembly</param>
        /// <param name="path">Path to the assembly</param>
        public AssemblyInfo(string name, string path)
        {
            Name = name;
            Path = path;
            FileName = System.IO.Path.GetFileName(path);
        }

        /// <summary>
        /// Gets the name of the assembly
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the fileName of the assembly
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the fullpath to the assembly
        /// </summary>
        public string Path { get; private set; }

        /// <inheritdoc/>
        public override bool Equals (object obj)
        {
            var other = obj as AssemblyInfo;
            return Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(AssemblyInfo other)
        {
            return other.Name == Name;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
