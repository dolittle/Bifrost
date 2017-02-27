/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
#if(NETFX_CORE)
using Windows.UI.Xaml;
#else
using System.Windows;
#endif
using Bifrost.Extensions;

namespace Bifrost.Values
{
    public class DependencyProperty<T1,T>
        where T1:DependencyObject
    {
        public DependencyProperty ActualDependencyProperty { get; private set; }
        public string PropertyName { get; private set; }

        public static implicit operator DependencyProperty(DependencyProperty<T1, T> property)
        {
            return property.ActualDependencyProperty;
        }

        private DependencyProperty(DependencyProperty dependencyProperty, string name)
        {
            this.ActualDependencyProperty = dependencyProperty;
            this.PropertyName = name;
        }


        public T GetValue(DependencyObject obj)
        {
            return obj.GetValue<T>(this.ActualDependencyProperty);
        }

        public void SetValue(DependencyObject obj, T value)
        {
            DependencyPropertyHelper.SetIsInternalSet(obj,true);
            obj.SetValue<T>(this.ActualDependencyProperty,value);
            DependencyPropertyHelper.SetIsInternalSet(obj, false);
        }

        public static DependencyProperty<T1,T> Register(Expression<Func<T1, T>> expression)
        {
            return Register(expression, default(T));
        }

        public static DependencyProperty<T1,T> Register(Expression<Func<T1, T>> expression, T defaultValue)
        {
            var propertyInfo = expression.GetPropertyInfo();

            var property = DependencyPropertyHelper.Register<T1, T>(expression,defaultValue);

            var typeSafeProperty = new DependencyProperty<T1, T>(property,propertyInfo.Name);

            return typeSafeProperty;
        }
    }
}
