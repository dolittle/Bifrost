/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;

namespace Bifrost.Logging
{
    /// <summary>
    /// Defines a convention for configuring <see cref="ILogAppenders"/>
    /// </summary>
    public interface ICanConfigureLogAppenders : IConvention
    {
        /// <summary>
        /// Configure <see cref="ILogAppenders"/>
        /// </summary>
        /// <param name="logAppenders"><see cref="ILogAppenders"/> to configure</param>
        void Configure(ILogAppenders logAppenders);
    }
}
