/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tenancy
{
    /// <summary>
    /// Defines a visitor that takes part in populating all the details for a tenant
    /// </summary>
    public interface ICanPopulateTenant
    {
        /// <summary>
        /// Method that gets called when the <see cref="Tenant"/> is being set up
        /// </summary>
        /// <param name="tenant"><see cref="Tenant"/> that is being populated</param>
        /// <param name="details">Details for the <see cref="Tenant"/> - can be expanded on</param>
        void Visit(Tenant tenant, dynamic details);
    }
}
