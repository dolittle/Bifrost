/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Execution;
using Ninject;
using Ninject.Modules;

namespace Bifrost.Ninject
{
    /// <summary>
    /// Extensions for working with <see cref="IKernel"/>
    /// </summary>
    public static class KernelExtensions
    {
        /// <summary>
        /// Discover all modules and load them into the kernal
        /// </summary>
        /// <param name="kernel"><see cref="IKernel"/> to load into</param>
        public static void LoadAllModules(this IKernel kernel)
        {
            var typeImporter = kernel.Get<ITypeImporter>();
            var modules = typeImporter.ImportMany<NinjectModule>();
            if( modules.Count() > 0 ) kernel.Load(modules);
        }
    }
}
