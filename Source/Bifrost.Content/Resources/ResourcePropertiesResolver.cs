/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Extensions;
using Bifrost.Execution;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents a <see cref="IResourcePropertiesResolver"/>
    /// </summary>
    public class ResourcePropertiesResolver : IResourcePropertiesResolver
    {
        IContainer _container;
        
        /// <summary>
        /// Initializes a new instance of <see cref="ResourcePropertiesResolver"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to use</param>
        public ResourcePropertiesResolver(IContainer container)
        {
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public void ResolvePropertiesFor<T>(T instance)
        {
            var propertiesQuery = from t in typeof (T).GetProperties()
                                  where t.PropertyType.HasInterface<IHaveResources>()
                                  select t;
            foreach (var property in propertiesQuery )
            {
                var stringsInstance = _container.Get(property.PropertyType);
                property.SetValue(instance, stringsInstance, null);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}