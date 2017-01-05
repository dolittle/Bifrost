/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Bifrost.Entities.Files;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuration
    /// </summary>
    public static partial class ConfigurationExtensions
    {

        /// <summary>
        /// Configures <see cref="IHaveStorage">storage</see> to use a simple files system
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage">Storage</see> to configure</param>
        /// <param name="path">Path to store files</param>
        /// <returns>Chained <see cref="IConfigure"/> for fluent configuration</returns>
        public static IConfigure UsingFiles(this IHaveStorage storage, string path)
        {
            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new EntityContextConfiguration
            {
                Path = path
            };

            configuration.Connection = new EntityContextConnection(configuration);

            storage.EntityContextConfiguration = configuration;

            return Configure.Instance;
        }
    }
}
