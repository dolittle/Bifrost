/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Raven.Client.Converters;

namespace Bifrost.RavenDB
{
    public class NullIdPropertyRegister : IEntityIdPropertyRegister
    {
        public void RegisterIdProperty<T,TId>(Expression<Func<T,TId>> property)
        { }

        public bool IsIdProperty(Type type, PropertyInfo propertyInfo)
        {
            return false;
        }

        public IEnumerable<ITypeConverter> GetTypeConvertersForConceptIds()
        {
            yield break;
        }
    }
}