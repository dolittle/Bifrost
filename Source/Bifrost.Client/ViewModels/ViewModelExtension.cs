/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using Bifrost.Configuration;
using Bifrost.Reflection;

namespace Bifrost.ViewModels
{
    [MarkupExtensionReturnType(typeof(object))]
    public class ViewModelExtension : MarkupExtension
    {
        Type _type;

        public ViewModelExtension(Type type)
        {
            _type = type;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (!(target.TargetObject is DependencyObject)) return this;

            var designMode = DesignerProperties.GetIsInDesignMode(target.TargetObject as DependencyObject);
            if (designMode)
            {
                var proxying = new Proxying();
                var proxy = proxying.BuildClassWithPropertiesFrom(_type);
                var instance = Activator.CreateInstance(proxy);
                return instance;
            }
            else
            {
                var instance = Configure.Instance.Container.Get(_type);
                return instance;
            }
        }
    }
}
