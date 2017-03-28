/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Extends <see cref="IApplicationStructureConfigurationBuilder"/> with more concrete concepts
    /// </summary>
    public static class ApplicationStructureBuilderConfigurationExtensions
    {

        /// <summary>
        /// Includes a convention format for the Domain aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureConfigurationBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureConfigurationBuilder Domain(this IApplicationStructureConfigurationBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Domain, format);
        }

        /// <summary>
        /// Includes a convention format for the Events aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureConfigurationBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureConfigurationBuilder Events(this IApplicationStructureConfigurationBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Events, format);
        }

        /// <summary>
        /// Includes a convention format for the Read aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureConfigurationBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureConfigurationBuilder Read(this IApplicationStructureConfigurationBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Read, format);
        }

        /// <summary>
        /// Includes a convention format for the Frontend aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureConfigurationBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureConfigurationBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureConfigurationBuilder Frontend(this IApplicationStructureConfigurationBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Frontend, format);
        }
    }
}
