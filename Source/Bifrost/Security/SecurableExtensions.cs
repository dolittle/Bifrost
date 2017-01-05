/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Extensions for <see cref="ISecurable"/>
    /// </summary>
    public static class SecurableExtensions
    {
        /// <summary>
        /// Define a user actor for a <see cref="ISecurable">securable</see>
        /// </summary>
        /// <param name="securable"><see cref="ISecurable"/> to secure</param>
        /// <returns>The <see cref="UserSecurityActor"/></returns>
        public static UserSecurityActor User(this ISecurable securable)
        {
            var actor = new UserSecurityActor();
            securable.AddActor(actor);
            return actor;
        }
    }
}
