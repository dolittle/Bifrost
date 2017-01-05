/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents a <see cref="ITenantManager"/>
    /// </summary>
    public class TenantManager : ITenantManager
    {
#pragma warning disable 1591 // Xml Comments
        public ITenant Current
        {
            get 
            {
                return null;
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
