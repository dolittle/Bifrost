/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;
using FluentValidation.Internal;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Resolves property names and display names, taking into account concepts and model rules
    /// </summary>
    public class NameResolvers
    {
        /// <summary>
        /// Use by Fluent Validation.  Resolves property names, taking into account concepts and model rules
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberInfo"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string PropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = FromExpression(type,memberInfo,expression);
                if (chain.Count > 0)
                {
                    var chainAsString = chain.ToString();
                    return chainAsString;
                }
            }

            if (memberInfo != null)
            {
                return (IsModelRule(memberInfo) || IsConcept(memberInfo)) 
                    ? string.Empty : memberInfo.Name;
            }

            return null;
        }

        /// <summary>
        /// Used by Fluent Validation.  Resolves display name based on member expression.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberInfo"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string DisplayNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression == null) return "[N/A]";
            var displayName = FromExpression(expression);
            return displayName;
        }

        static PropertyChain FromExpression(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            var stack = new Stack<string>();

            if (type.IsConcept())
                return new PropertyChain(stack);

            for (var memberExpression = ExpressionExtensions.Unwrap(expression.Body);
                    memberExpression != null;
                    memberExpression = ExpressionExtensions.Unwrap(memberExpression.Expression))
            {
                if(!IsConcept(memberInfo))
                    stack.Push(memberExpression.Member.Name);
            }
            return new PropertyChain((IEnumerable<string>)stack);
        }

        static string FromExpression(LambdaExpression expression)
        {
            var stack = new Stack<string>();
            for (var memberExpression = ExpressionExtensions.Unwrap(expression.Body);
                    memberExpression != null;
                    memberExpression = ExpressionExtensions.Unwrap(memberExpression.Expression))
            {
                stack.Push(memberExpression.Member.Name);
            }
            return string.Join(".", stack.ToArray());
        }

        static bool IsModelRule(MemberInfo memberInfo)
        {
            return memberInfo.DeclaringType.IsModelRuleProperty();
        }

        static bool IsConcept(MemberInfo memberInfo)
        {
            return memberInfo.DeclaringType.IsConcept();
        }
    }
}