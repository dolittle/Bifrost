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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Bifrost.Extensions;
#if(NETFX_CORE)
using Windows.UI.Xaml;
using Windows.UI.Core;
#else
using System.Windows;
#endif

namespace Bifrost.Values
{
	public static class DependencyPropertyHelper
	{
		public static readonly DependencyProperty IsInternalSetProperty =
			DependencyProperty.RegisterAttached("IsInternalSet", typeof(bool), typeof(DependencyPropertyHelper), null);

		public static readonly DependencyProperty IsNotFirstSetProperty =
			DependencyProperty.RegisterAttached("IsNotFirstSet", typeof (bool), typeof (DependencyPropertyHelper), null);

		public static void SetIsInternalSet(DependencyObject obj, bool value)
		{
			obj.SetValue(IsInternalSetProperty, value);
		}

		public static bool GetIsInternalSet(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsInternalSetProperty);
		}

		public static void SetIsNotFirstSet(DependencyObject obj, bool value)
		{
			obj.SetValue(IsNotFirstSetProperty,value);
		}

		public static bool GetIsNotFirstSet(DependencyObject obj)
		{
			return (bool) obj.GetValue(IsNotFirstSetProperty);
		}


		private static PropertyMetadata GetPropertyMetaData(PropertyInfo propertyInfo, object defaultValue)
		{
			var propertyMetadata = new PropertyMetadata(defaultValue,
			                                            (o, e) =>
			                                            	{
			                                            		if (GetIsInternalSet(o))
			                                            		{
			                                            			return;
			                                            		}
			                                            		if (null == e.OldValue || (!e.OldValue.Equals(e.NewValue)||!GetIsNotFirstSet(o)) )
			                                            		{
			                                            			SetIsNotFirstSet(o,true);
			                                            			Action a = () => propertyInfo.SetValue(o, e.NewValue, null);

#if(NETFX_CORE)
                                                                    if (o.Dispatcher.HasThreadAccess) a();
                                                                    else o.Dispatcher.RunIdleAsync(ide => a());
#else
			                                            			if (o.Dispatcher.CheckAccess()) a();
			                                            			else o.Dispatcher.BeginInvoke(a);
#endif
			                                            		}
			                                            	});
			return propertyMetadata;
		}


		public static DependencyProperty Register<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			return Register<T, TResult>(expression, default(TResult));
		}

		public static DependencyProperty Register<T, TResult>(Expression<Func<T, TResult>> expression, TResult defaultValue)
		{
			var propertyInfo = expression.GetPropertyInfo();

			var prop = DependencyProperty.Register(
				propertyInfo.Name,
				propertyInfo.PropertyType,
				typeof(T),
				GetPropertyMetaData(propertyInfo, defaultValue));

			return prop;
		}


		private static object GetDependencyPropertyByPropertyName(Type root, string propertyName, out string propertyPath)
		{

			if (root.Equals(typeof(object)))
			{
				propertyPath = string.Empty;
				return null;
			}
#if(NETFX_CORE)
            var fields = root.GetTypeInfo().DeclaredFields.Where(f => f.IsStatic && f.IsPublic);
#else
			var fields = root.GetFields(BindingFlags.Static | BindingFlags.Public);
#endif
			foreach (var field in fields)
			{
				if (field.Name.Contains(propertyName))
				{
					var dependencyProperty = field.GetValue(null);

					propertyPath = root.Name + "." + propertyName;

					return dependencyProperty;
				}
			}

#if(NETFX_CORE)
            var baseType = root.GetTypeInfo().BaseType;
#else
            var baseType = root.BaseType;
#endif
			var property = GetDependencyPropertyByPropertyName(baseType, propertyName, out propertyPath);
			return property;
		}


		public static DependencyProperty GetDependencyPropertyByPropertyName(FrameworkElement element, string propertyName, out string propertyPath)
		{
			var type = element.GetType();
			var fieldValue = GetDependencyPropertyByPropertyName(type, propertyName, out propertyPath);

			if (fieldValue is DependencyProperty)
			{
				return fieldValue as DependencyProperty;
			}
			else
			{
#if(NETFX_CORE)
                var property = fieldValue.GetType().GetTypeInfo().DeclaredProperties.Single(p => p.Name == "ActualDependencyProperty");
#else
				var property = fieldValue.GetType().GetProperty("ActualDependencyProperty");
#endif
				if (null != property)
				{
					var actualDependencyProperty = property.GetValue(fieldValue, null) as DependencyProperty;
					return actualDependencyProperty;
				}


			}
			return null;
		}
	}
}
