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
    public interface IEntityIdPropertyRegister
    {
        void RegisterIdProperty<T,TId>(Expression<Func<T,TId>> property);
        bool IsIdProperty(Type type, PropertyInfo propertyInfo);
        IEnumerable<ITypeConverter> GetTypeConvertersForConceptIds();
    }
}