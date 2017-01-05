/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
