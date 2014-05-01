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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
#if(NETFX_CORE)
using Windows.UI.Xaml;
#else
using System.Windows;
#endif
using Bifrost.Extensions;

namespace Bifrost.Values
{
	public static class DependencyPropertyExtensions
	{
		public static void SetValue<T>(this DependencyObject obj, DependencyProperty property, T value)
		{
			object oldValue = obj.GetValue(property);
			if (null != oldValue && null != value)
			{
				if (oldValue.Equals(value))
				{
					return;
				}
			}
			obj.SetValue(property, value);
		}

		public static T GetValue<T>(this DependencyObject obj, DependencyProperty property)
		{
			return (T)obj.GetValue(property);
		}

		public static DependencyProperty GetDependencyPropertyByPropertyName(this DependencyObject obj, string propertyName)
		{
			var dependencyPropertyName = string.Format("{0}Property", propertyName);
#if(NETFX_CORE)
            var dependencyProperty = obj.GetType().GetTypeInfo().DeclaredFields.Single(f => f.Name == dependencyPropertyName && f.IsPublic && f.IsStatic);
#else 
			var dependencyProperty = obj.GetType().GetField(dependencyPropertyName, BindingFlags.Static | BindingFlags.Public);
#endif
			if( null == dependencyProperty )
			{
				throw new ArgumentException("Property '"+propertyName+"' does not exist");
			}
			if( !dependencyProperty.FieldType.Equals(typeof(DependencyProperty)) )
			{
				throw new ArgumentException("Property '"+propertyName+"' is not a DependencyProperty");
			}

			return dependencyProperty.GetValue(null) as DependencyProperty;
		}


		private static readonly Dictionary<DependencyObject, IDependencyPropertySubscription> ExpressionSubscriptions = new Dictionary<DependencyObject, IDependencyPropertySubscription>();

		public static void AddChangeHandler<T>(this T element, DependencyProperty dependencyProperty, PropertyChangedHandler<T> handler)
			where T : FrameworkElement
		{

			var key = element; 
			IDependencyPropertySubscription subscription = null;
			if (ExpressionSubscriptions.ContainsKey(key))
			{
				subscription = ExpressionSubscriptions[key];
			}
			else
			{
				subscription = new DependencyPropertySubscription<T>((T)element, dependencyProperty);
				ExpressionSubscriptions[key] = subscription;
			}

			((INotifyPropertyChanged) subscription).SubscribeToChange(() => subscription.Value,  o => handler((T)element));
		}


		public static void AddChangeHandler<T>(this T element, Expression<Func<object>> expression, PropertyChangedHandler<T> handler)
			where T : FrameworkElement
		{
			string propertyPath;
			var propertyInfo = expression.GetPropertyInfo();
			var dependencyProperty = DependencyPropertyHelper.GetDependencyPropertyByPropertyName(element, propertyInfo.Name, out propertyPath);
			AddChangeHandler(element,dependencyProperty,handler);
		}
	}
}
