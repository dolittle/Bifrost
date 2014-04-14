#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Windows;
using Bifrost.Extensions;


namespace Bifrost.Helpers
{
	public class DependencyProperty<T1,T>
		where T1:DependencyObject
	{
		public DependencyProperty ActualDependencyProperty { get; private set; }
		public string PropertyName { get; private set; }

		public static implicit operator DependencyProperty(DependencyProperty<T1, T> property)
		{
			return property.ActualDependencyProperty;
		}

		private DependencyProperty(DependencyProperty dependencyProperty, string name)
		{
			this.ActualDependencyProperty = dependencyProperty;
			this.PropertyName = name;
		}


		public T GetValue(DependencyObject obj)
		{
			return obj.GetValue<T>(this.ActualDependencyProperty);
		}

		public void SetValue(DependencyObject obj, T value)
		{
			DependencyPropertyHelper.SetIsInternalSet(obj,true);
			obj.SetValue<T>(this.ActualDependencyProperty,value);
			DependencyPropertyHelper.SetIsInternalSet(obj, false);
		}

		public static DependencyProperty<T1,T> Register(Expression<Func<T1, T>> expression)
		{
			return Register(expression, default(T));
		}

		public static DependencyProperty<T1,T> Register(Expression<Func<T1, T>> expression, T defaultValue)
		{
			var propertyInfo = expression.GetPropertyInfo();

			var property = DependencyPropertyHelper.Register<T1, T>(expression,defaultValue);

			var typeSafeProperty = new DependencyProperty<T1, T>(property,propertyInfo.Name);

			return typeSafeProperty;
		}
	}
}
