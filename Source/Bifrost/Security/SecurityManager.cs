/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityManager"/>
    /// </summary>
    [Singleton]
    public class SecurityManager : ISecurityManager
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;
        IEnumerable<ISecurityDescriptor> _securityDescriptors;

        /// <summary>
        /// Initializes a new instance of <see cref="SecurityManager"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to discover any <see cref="BaseSecurityDescriptor">security descriptors</see></param>
        /// <param name="container"><see cref="IContainer"/> to instantiate instances of <see cref="ISecurityDescriptor"/></param>
        public SecurityManager(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;

            PopulateSecurityDescriptors();
        }

        void PopulateSecurityDescriptors()
        {
            var securityDescriptorTypes = _typeDiscoverer.FindMultiple<ISecurityDescriptor>();
            var instances = new List<ISecurityDescriptor>();
            instances.AddRange(securityDescriptorTypes.Select(t => _container.Get(t) as ISecurityDescriptor));
            _securityDescriptors = instances;
        }

#pragma warning disable 1591 // Xml Comments
        public AuthorizationResult Authorize<T>(object target) where T : ISecurityAction
        {
            var result = new AuthorizationResult();
            if (!_securityDescriptors.Any())
                return result;

            var applicableSecurityDescriptors = _securityDescriptors.Where(sd => sd.CanAuthorize<T>(target));

            if (!applicableSecurityDescriptors.Any())
                return result;

            foreach(var securityDescriptor in applicableSecurityDescriptors)
                result.ProcessAuthorizeDescriptorResult(securityDescriptor.Authorize(target));

            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
