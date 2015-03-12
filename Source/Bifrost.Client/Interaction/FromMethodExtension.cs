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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Bifrost.Interaction
{
    /// <summary>
    /// Markup extension for connecting a method on a viewModel as a command
    /// </summary>
    [MarkupExtensionReturnType(typeof(ICommand))]
    public class FromMethodExtension : MarkupExtension
    {
        string _name;

        /// <summary>
        /// Initializes a new instance of <see cref="FromMethodExtension"/>
        /// </summary>
        /// <param name="name"></param>
        public FromMethodExtension(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets or sets the name of the property or method to call for checking if it can execute
        /// </summary>
        /// <remarks>
        /// If it is a property and the declaring type implements <see cref="INotifyPropertyChanged"/>,
        /// the extension will subscribe to it and get any changes.
        /// </remarks>
        public string CanExecuteWhen { get; set; }

#pragma warning disable 1591
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                if (!(target.TargetObject is DependencyObject)) return this;


                var element = target.TargetObject as FrameworkElement;
                var targetProperty = target.TargetProperty as DependencyProperty;
                if (element == null || targetProperty == null) return null;

                var viewModel = GetDataContext(element);
                if (viewModel == null)
                {
                    element.DataContextChanged += (s, e) =>
                    {
                        viewModel = e.NewValue;
                        var command = new CommandForMethod(viewModel, _name, CanExecuteWhen);
                        element.SetValue(targetProperty, command);
                    };
                }
                else
                {
                    var command = new CommandForMethod(viewModel, _name, CanExecuteWhen);
                    return command;
                }
            }
            catch { }

            return null;
        }
#pragma warning restore 1591

        object GetDataContext(object target)
        {
            var element = target as FrameworkElement;
            if (element == null)
                return null;

            return element.GetValue(FrameworkElement.DataContextProperty)
                ?? element.GetValue(FrameworkContentElement.DataContextProperty);
        }
    }
}
