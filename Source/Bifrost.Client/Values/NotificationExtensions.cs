/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Execution;

namespace Bifrost.Values
{
    

    public static class NotificationExtensions
    {
        public static void Notify(this PropertyChangedEventHandler eventHandler, Expression<Func<object>> expression)
        {
            if (eventHandler == null || !DispatcherManager.HasBeenSet ) return;
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            var constantExpression = memberExpression.Expression as ConstantExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;


            Bifrost.Execution.DispatcherManager.Current.BeginInvoke(
                () =>
                    {
                        foreach (var del in eventHandler.GetInvocationList())
                        {
                            try
                            {
                                del.DynamicInvoke(new object[] { constantExpression.Value, new PropertyChangedEventArgs(propertyInfo.Name) });
                            }
                            catch
                            {
                            }
                        }
                    });
        }

        public static void SubscribeToChange<T>(this T objectThatNotifies, Expression<Func<object>> expression, PropertyChangedHandler<T> handler)
            where T : INotifyPropertyChanged
        {
            objectThatNotifies.PropertyChanged +=
                (s, e) =>
                    {
                        var lambda = expression as LambdaExpression;
                        MemberExpression memberExpression;
                        if (lambda.Body is UnaryExpression)
                        {
                            var unaryExpression = lambda.Body as UnaryExpression;
                            memberExpression = unaryExpression.Operand as MemberExpression;
                        }
                        else
                        {
                            memberExpression = lambda.Body as MemberExpression;
                        }
                        var propertyInfo = memberExpression.Member as PropertyInfo;

                        if (e.PropertyName.Equals(propertyInfo.Name))
                        {
                            handler(objectThatNotifies);
                        }
                    };
        }

        public static void SubscribeToChange(this INotifyPropertyChanged objectThatNotifies, Expression<Func<object>> expression, PropertyChangedHandler<INotifyPropertyChanged> handler)
        {
            SubscribeToChange<INotifyPropertyChanged>(objectThatNotifies, expression, handler);
        }

        public static void SubscribeToChange(this INotifyPropertyChanged objectThatNotifies, string propertyName, PropertyChangedHandler<INotifyPropertyChanged> handler)
        {
            objectThatNotifies.PropertyChanged +=
                (s, e) =>
                    {
                        if (e.PropertyName.Equals(propertyName))
                        {
                            handler(objectThatNotifies);
                        }
                    };
        }
    }
}
