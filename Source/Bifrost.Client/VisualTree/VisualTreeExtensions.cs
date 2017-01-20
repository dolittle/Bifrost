﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Windows;


#if(NETFX_CORE)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Bifrost.VisualTree
{
    public static class VisualTreeExtensions
    {
        public static void RemoveChild(this DependencyObject parent, UIElement child)
        {
            var panel = parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(child);
                return;
            }

#if(!NETFX_CORE)
            var decorator = parent as Decorator;
            if (decorator != null)
            {
                if (decorator.Child == child)
                {
                    decorator.Child = null;
                }
                return;
            }
#endif

            var contentPresenter = parent as ContentPresenter;
            if (contentPresenter != null)
            {
                if (contentPresenter.Content == child)
                {
                    contentPresenter.Content = null;
                }
                return;
            }

            var contentControl = parent as ContentControl;
            if (contentControl != null)
            {
                if (contentControl.Content == child)
                {
                    contentControl.Content = null;
                }
                return;
            }

            // maybe more
        }

        public static UIElement[] GetAllUIElements(this Panel parentPanel)
        {
            var elements = new List<UIElement>();
            if (null != parentPanel)
            {
                GetAllUIElements(parentPanel, elements);
            }
            return elements.ToArray();
        }

        private static void GetAllUIElements(Panel parent, ICollection<UIElement> elements)
        {
            elements.Add(parent);
            foreach (var element in parent.Children)
            {
                if (element is UIElement) elements.Add((UIElement)element);

                if (element is Panel)
                {
                    GetAllUIElements(element as Panel, elements);
                }
                else if (element is ContentControl)
                {
                    var contentElement = element as ContentControl;
                    if (contentElement.Content is Panel)
                    {
                        GetAllUIElements(contentElement.Content as Panel, elements);
                    }
                    else if (contentElement.Content is UIElement)
                    {
                        elements.Add(contentElement.Content as UIElement);
                    }
                }
                else if (element is UIElement)
                {
                    elements.Add(element as UIElement);
                }
            }
        }

        public static T FindVisualParent<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                T typed = parent as T;
                if (typed != null)
                {
                    return typed;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}