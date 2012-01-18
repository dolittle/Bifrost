#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Bifrost.Helpers;
using DependencyProperty = System.Windows.DependencyProperty;

namespace Bifrost.Extensions
{
    public class DependencyPropertySubscription<T> : FrameworkElement, IDependencyPropertySubscription
		where T:FrameworkElement
	{
		public T Element { get; private set; }
		public DependencyProperty DependencyProperty { get; private set; }
		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

		public DependencyPropertySubscription(T element, DependencyProperty dependencyProperty)
		{
			Element = element;
			DependencyProperty = dependencyProperty;

			var sourceBinding = new System.Windows.Data.Binding("Value") { Source = this, Mode = BindingMode.TwoWay };
			element.SetBinding(DependencyProperty, sourceBinding);
		}


		private static readonly DependencyProperty<DependencyPropertySubscription<T>, object> ValueProperty =
			DependencyProperty<DependencyPropertySubscription<T>, object>.Register(o => o.Value);
		public object Value
		{
			get { return ValueProperty.GetValue(this); }
			set
			{
				ValueProperty.SetValue(this, value);
				PropertyChanged.Notify(()=>Value);
			}
		}
	}
}
#endif