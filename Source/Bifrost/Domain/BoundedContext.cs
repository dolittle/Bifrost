/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContext"/>
    /// </summary>
    public class BoundedContext : IBoundedContext
    {
        List<IModule> _modules = new List<IModule>();

        /// <summary>
        /// Initializes a new instance of <see cref="Bounded"/>
        /// </summary>
        public BoundedContext(BoundedContextName name)
        {
            Name = name;
        }

        /// <inheritdoc/>
        public IEnumerable<IModule> Modules => _modules;

        /// <inheritdoc/>
        public BoundedContextName Name { get; }

        /// <inheritdoc/>
        public void AddModule(IModule module)
        {
            ThrowIfModuleAlreadyAdded(module);
            _modules.Add(module);
        }

        void ThrowIfModuleAlreadyAdded(IModule module)
        {
            if (_modules.Contains(module)) throw new ModuleAlreadyAddedToBoundedContext(this, module);
        }
    }
}
