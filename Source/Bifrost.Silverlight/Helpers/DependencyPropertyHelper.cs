#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
#if(SILVERLIGHT)
using System;
using System.Reflection;
using System.Windows;
using System.Linq.Expressions;
using Bifrost.Extensions;

namespace Bifrost.Helpers
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
			                                            			if (o.Dispatcher.CheckAccess())
			                                            			{
			                                            				a();
			                                            			}
			                                            			else
			                                            			{
			                                            				o.Dispatcher.BeginInvoke(a);
			                                            			}
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
			var fields = root.GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (var field in fields)
			{
				if (field.Name.Contains(propertyName))
				{
					var dependencyProperty = field.GetValue(null);

					propertyPath = root.Name + "." + propertyName;

					return dependencyProperty;
				}
			}

			var property = GetDependencyPropertyByPropertyName(root.BaseType, propertyName, out propertyPath);
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
				var property = fieldValue.GetType().GetProperty("ActualDependencyProperty");
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
#endif