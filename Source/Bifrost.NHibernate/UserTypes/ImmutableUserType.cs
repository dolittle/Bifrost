/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// Helps create NHibernate Custom Mappings for Types
    /// </summary>
    /// <typeparam name="T">Type that this user type maps</typeparam>
    public abstract class ImmutableUserType<T> : UserTypeBase<T>
    {
#pragma warning disable 1591 // Xml Comments
        protected override T CreateInstance(object[] propertyValues)
        {
            var instance = Activator.CreateInstance<T>();

            for (var i = 0; i < propertyValues.Length; i++)
            {
                Properties[i].SetValue(instance, propertyValues[i], null);
            }

            return instance;
        }

        public override bool IsMutable
        {
            get { return false; }
        }

        protected override T PerformDeepCopy(T source)
        {
            return source;
        }
    }
#pragma warning restore // Xml Comments
}