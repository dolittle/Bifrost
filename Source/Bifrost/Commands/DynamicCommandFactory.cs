#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Linq.Expressions;
using Bifrost.Domain;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents an implementation of <see cref="IDynamicCommandFactory"/>
	/// </summary>
    public class DynamicCommandFactory : IDynamicCommandFactory
	{
#pragma warning disable 1591 // Xml Comments
		public DynamicCommand Create<T>(Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot
        {
            if (!(method.Body is MethodCallExpression))
                throw new ExpressionNotMethodCallException(method.ToString());

            var methodCallExpression = method.Body as MethodCallExpression;
            if( methodCallExpression == null )
                throw new ExpressionNotMethodCallException(method.ToString());

            

            var command = new DynamicCommand { Id = aggregatedRootId, Method = methodCallExpression.Method, AggregatedRootType = typeof(T) };
            var parameters = methodCallExpression.Method.GetParameters();

            for (var argumentIndex = 0; argumentIndex<methodCallExpression.Arguments.Count; argumentIndex++ )
            {
                var argument = methodCallExpression.Arguments[argumentIndex];
                var parameter = parameters[argumentIndex];
                var value = Expression.Lambda(argument).Compile().DynamicInvoke();
                command.SetValue(parameter.Name, value);
            }

            return command;
		}
#pragma warning restore 1591 // Xml Comments
	}
}