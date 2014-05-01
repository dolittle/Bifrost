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

using System.ComponentModel;
#if(NETFX_CORE)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using DependencyProperty = Windows.UI.Xaml.DependencyProperty;
#else
using System.Windows;
using System.Windows.Data;
using DependencyProperty = System.Windows.DependencyProperty;
#endif

namespace Bifrost.Values
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

			var sourceBinding = new Binding() { Path = new PropertyPath("Value"), Source = this, Mode = BindingMode.TwoWay };
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
