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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using Bifrost.Values;

namespace Bifrost.Interaction
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommand"/> for a method on a target, 
    /// typically a ViewModel
    /// </summary>
    public class CommandForMethod : ICommand, IDisposable
    {
        MethodInfo _method;
        object _target;
        string _canExecuteWhen;
        IValueConverter _parameterConverter;
        Func<object, object, bool> _canExecuteFunc;
        bool _isSubscribingForChanges;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandForMethod"/>
        /// </summary>
        /// <param name="target">Target to bind to for the method, typically a ViewModel</param>
        /// <param name="methodName">Name of the method to bind to</param>
        /// <param name="canExecuteWhen">Optionally a reference to either a property or method that gets called to check if the command can execute</param>
        /// <param name="parameterConverter">Optionally a <see cref="IValueConverter"/> that will be used for converting value to the method</param>
        /// <remarks>
        /// The canExecuteWhen parameter can take the name of a property or a method. If it is 
        /// a property and the declaring type implements <see cref="INotifyPropertyChanged"/>,
        /// it will honor that and actually fire off the CanExecuteChanged if the property changes
        /// state. The property needs to be of type boolean, and a method can take one parameter,
        /// and the CommandParameter property from the view will be the content if so. The method
        /// needs to return a boolean.
        /// </remarks>
        public CommandForMethod(object target, string methodName, string canExecuteWhen = null, IValueConverter parameterConverter = null)
        {
            _target = target;
            _canExecuteWhen = canExecuteWhen;
            _parameterConverter = parameterConverter;
            GetMethod(target, methodName);
            if (!string.IsNullOrEmpty(canExecuteWhen)) SetupCanExecuteWhen(target, canExecuteWhen);
        }

#pragma warning disable 1591
        public event EventHandler CanExecuteChanged = (s, e) => { };

        public bool CanExecute(object parameter)
        {
            if (_canExecuteFunc != null) return _canExecuteFunc(_target, parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            var parameters = _method.GetParameters();
            if( parameters.Length == 0 ) _method.Invoke(_target,new object[0]);
            else _method.Invoke(_target, new object[] { 
                ConvertParameterIfValueConverterSpecified(parameters[0], parameter)
            });
        }


        public void Dispose()
        {
            if (_isSubscribingForChanges) ((INotifyPropertyChanged)_target).PropertyChanged -= CommandForMethod_PropertyChanged;
        }
#pragma warning restore 1591

        object ConvertParameterIfValueConverterSpecified(ParameterInfo parameterInfo, object parameter)
        {
            if (_parameterConverter != null) return _parameterConverter.Convert(parameter, parameterInfo.ParameterType, null, CultureInfo.CurrentUICulture);
            return parameter;
        }

        void GetMethod(object target, string methodName)
        {
            var type = target.GetType();
            var methods = type.GetMethods();
            _method = methods.SingleOrDefault(m => m.Name == methodName);
            ThrowIfMethodIsMissing(methodName, type, _method);
            var parameters = _method.GetParameters();
            ThrowIfMoreThanOneParameter(parameters, methodName, type);
        }


        void SetupCanExecuteWhen(object target, string canExecuteWhen)
        {
            var type = target.GetType();

            var methods = type.GetMethods();
            var method = methods.SingleOrDefault(m => m.Name == canExecuteWhen);
            if (method != null)
                SetupCanExecuteWhenForMethod(type, method);
            else
                SetupCanExecuteWhenForProperty(target, canExecuteWhen, type);
        }

        private void SetupCanExecuteWhenForMethod(Type type, MethodInfo method)
        {
            ThrowIfReturnTypeNotBoolean(type, method, method.ReturnType);
            var parameters = method.GetParameters();
            ThrowIfMoreThanOneParameter(parameters, method.Name, type);
            if (parameters.Length == 1) _canExecuteFunc = (t, p) => (bool)method.Invoke(t, new object[] { 
                ConvertParameterIfValueConverterSpecified(parameters[0], p)
            });
            else _canExecuteFunc = (t, p) => (bool)method.Invoke(t, new object[0]);
        }

        private void SetupCanExecuteWhenForProperty(object target, string canExecuteWhen, Type type)
        {
            var property = type.GetProperty(canExecuteWhen);
            ThrowIfMissingMethodAndProperty(canExecuteWhen, type, property);
            ThrowIfReturnTypeNotBoolean(type, property, property.PropertyType);

            _canExecuteFunc = (t, p) => (bool)property.GetValue(t);

            if (target is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)target).PropertyChanged += CommandForMethod_PropertyChanged;
                _isSubscribingForChanges = true;
            }
        }

        void CommandForMethod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _canExecuteWhen) CanExecuteChanged(this, new EventArgs());
        }

        void ThrowIfMethodIsMissing(string methodName, Type type, MethodInfo _method)
        {
            if (_method == null) throw new MissingMethodForCommand(type, methodName);
        }

        void ThrowIfMoreThanOneParameter(ParameterInfo[] parameters, string methodName, Type type)
        {
            if (parameters.Length > 1) throw new MoreThanOneParameter(type, methodName);
        }

        void ThrowIfMissingMethodAndProperty(string canExecuteWhen, Type type, PropertyInfo property)
        {
            if (property == null) throw new MissingMethodOrPropertyForCanExecute(canExecuteWhen, type);
        }

        void ThrowIfReturnTypeNotBoolean(Type type, MemberInfo member, Type returnType)
        {
            if (returnType != typeof(bool)) throw new ReturnValueShouldBeBoolean(member.Name, type);
        }
    }
}
