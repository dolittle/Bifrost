/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Bifrost.Extensions
{
#pragma warning disable 1591 // Xml Comments
    public static class MethodCalls
    {
        public static TOut CallGenericMethod<TOut, T, T1, T2, T3>(this T target, Expression<Func<T, Func<T1, T2, T3, TOut>>> method, T1 param1, T2 param2, T3 param3, params Type[] genericArguments)
        {
            return CallGenericMethod<T, TOut>(target, method, new object[] { param1, param2, param3 }, genericArguments);
        }

        public static TOut CallGenericMethod<TOut, T, T1, T2>(this T target, Expression<Func<T, Func<T1, T2, TOut>>> method, T1 param1, T2 param2, params Type[] genericArguments)
        {
            return CallGenericMethod<T, TOut>(target, method, new object[] { param1, param2 }, genericArguments);
        }

        public static TOut CallGenericMethod<TOut, T, T1>(this T target, Expression<Func<T, Func<T1, TOut>>> method, T1 param1, params Type[] genericArguments)
        {
            return CallGenericMethod<T, TOut>(target, method, new object[] { param1 }, genericArguments);
        }

        public static TOut CallGenericMethod<TOut, T>(this T target, Expression<Func<T, Func<TOut>>> method, params Type[] genericArguments)
        {
            return CallGenericMethod<T, TOut>(target, method, new object[0], genericArguments);
        }

        static TOut CallGenericMethod<T, TOut>(this T target, Expression method, object[] parameters, Type[] genericArguments)
        {
            var lambda = method as LambdaExpression;
            var unary = lambda.Body as UnaryExpression;
            var methodCall = unary.Operand as MethodCallExpression;
            var constant = methodCall.Object as ConstantExpression;

            var methodInfo = constant.Value as MethodInfo;
            var genericMethodDefinition = methodInfo.GetGenericMethodDefinition();

            var genericMethod = genericMethodDefinition.MakeGenericMethod(genericArguments);

            var result = genericMethod.Invoke(target, parameters);
            return (TOut)result;
        }

        public class Generify<T>
        {
            public TOut Invoke<TOut>(Expression<Func<T, Func<TOut>>> method, params Type[] genericArguments)
            {
                throw new NotImplementedException();
            }

            public TOut Invoke<TOut, TParam1>(Expression<Func<T, Func<TParam1, TOut>>> method, TParam1 param1, params Type[] genericArguments)
            {
                throw new NotImplementedException();
            }
        }

        public static Generify<T> Generics<T>(this T g)
        {
            return new Generify<T>();
        }
    }
#pragma warning restore 1591 // Xml Comments
}
