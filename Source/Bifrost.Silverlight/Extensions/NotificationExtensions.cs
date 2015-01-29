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
using System.ComponentModel;
using System.Reflection;
using System.Linq.Expressions;
using System.Windows;

namespace Bifrost.Extensions
{
	public delegate void PropertyChangedHandler<T>(T sender);

	public static class NotificationExtensions
	{
		public static void Notify(this PropertyChangedEventHandler eventHandler, Expression<Func<object>> expression)
		{
			if (null == eventHandler)
			{
				return;
			}
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

            Deployment.Current.Dispatcher.BeginInvoke(
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
