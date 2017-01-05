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
    public static class KernelExtensions
    {
        public static void LoadAllModules(this IKernel kernel)
        {
            var typeImporter = kernel.Get<ITypeImporter>();
            var modules = typeImporter.ImportMany<NinjectModule>();
            if( modules.Count() > 0 ) kernel.Load(modules);
        }
    }
}
