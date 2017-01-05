/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionContextConfiguration"/>
    /// </summary>
    public class ExecutionContextConfiguration : IExecutionContextConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public void Initialize(IContainer container)
        {
            container.Bind<IExecutionContext>(() => container.Get<IExecutionContextManager>().Current, container.DefaultLifecycle);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
