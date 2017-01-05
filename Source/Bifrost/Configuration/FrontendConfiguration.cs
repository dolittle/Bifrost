/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of a <see cref="IFrontendConfiguration"/>
    /// </summary>
    public class FrontendConfiguration : IFrontendConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public IFrontendTargetConfiguration Target { get; set; }

        public void Initialize(IContainer container)
        {
            if (Target != null)
                Target.Initialize(container);

        }
#pragma warning restore 1591 // Xml Comments
    }
}
