/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Represents all the well defined areas recognized internally
    /// </summary>
    public class ApplicationAreas
    {
        /// <summary>
        /// The <see cref="ApplicationArea"/> for the domain
        /// </summary>
        public static ApplicationArea Domain = "Domain";

        /// <summary>
        /// The <see cref="ApplicationArea"/> for the read
        /// </summary>
        public static ApplicationArea Read = "Read";

        /// <summary>
        /// The <see cref="ApplicationArea"/> for the events
        /// </summary>
        public static ApplicationArea Events = "Events";

        /// <summary>
        /// The <see cref="ApplicationArea"/> for the frontend
        /// </summary>
        public static ApplicationArea Frontend = "Frontend";
    }
}
