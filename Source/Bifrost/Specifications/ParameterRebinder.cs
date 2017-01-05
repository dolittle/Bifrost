/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Helper for combining Lambdas.
    /// Ensures that the combined expression points at the same parameters where these are common.
    /// </summary>
    ///  <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class ParameterRebinder : ExpressionVisitor
    {
        readonly Dictionary<ParameterExpression, ParameterExpression> map;
        
        ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
                p = replacement;
            return base.VisitParameter(p);
        }
    }
}