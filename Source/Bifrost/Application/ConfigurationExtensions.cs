/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Application;

namespace Bifrost.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="builderCallback"></param>
        /// <returns></returns>
        public static IConfigure    Application(this IConfigure configure, Func<IApplicationConfigurationBuilder, IApplicationConfigurationBuilder> builderCallback)
        {
            return configure;
        }
    }
}
