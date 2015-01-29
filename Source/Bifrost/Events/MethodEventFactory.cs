#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bifrost.Events
{
	/// <summary>
	/// Provides functionality for creating method events
	/// </summary>
	public static class MethodEventFactory
	{
	    /// <summary>
	    /// Create a <see cref="MethodEvent">MethodEvent</see> from a method <see cref="Expression{Action}">expression</see>
	    /// </summary>
	    /// <param name="eventSourceId">Id of the event source</param>
	    /// <param name="method">Expression holding the method to create from</param>
	    /// <returns>A <see cref="MethodEvent">MethodEvent</see></returns>
	    public static MethodEvent CreateMethodEventFromExpression(Guid eventSourceId, Expression<Action> method)
		{
			var expression = method as LambdaExpression;
			if (null != expression &&
				expression.Body is MethodCallExpression)
			{
				var methodCall = expression.Body as MethodCallExpression;
				var methodEvent = CreateMethodEventFromMethodCallExpression(eventSourceId, methodCall);
				return methodEvent;
			}
			return null;
		}


		private static MethodEvent CreateMethodEventFromMethodCallExpression(Guid eventSourceId, MethodCallExpression expression)
		{
			var methodEvent = new MethodEvent(eventSourceId, expression.Method);
			AddArgumentsFromMethodCallExpression(expression, methodEvent);
			return methodEvent;
		}

		private static void AddArgumentsFromMethodCallExpression(MethodCallExpression expression, MethodEvent methodEvent)
		{
			var parameterInfos = expression.Method.GetParameters();

			for (var argumentIndex = 0; argumentIndex < expression.Arguments.Count(); argumentIndex++)
			{
				var parameterName = parameterInfos[argumentIndex].Name;
				var argument = expression.Arguments[argumentIndex] as MemberExpression;
				if (null != argument)
				{
					var fieldInfo = argument.Member as FieldInfo;
					if (null != fieldInfo)
					{
						var constantExpression = argument.Expression as ConstantExpression;
						if (null != constantExpression)
						{
							methodEvent.Arguments[parameterName] = fieldInfo.GetValue(constantExpression.Value);
						}
					}
				}
			}
		}
	}
}