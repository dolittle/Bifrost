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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Bifrost.Dynamic
{
    public class BindableExpandoType : Type
    {
        Type _baseType;
        BindableExpandoObject _target;

        public BindableExpandoType(Type delegatingType, BindableExpandoObject target)
        {
            _baseType = delegatingType;
            _target = target;
        }
        public override Assembly Assembly
        {
            get { return _baseType.Assembly; }
        }

        public override string AssemblyQualifiedName
        {
            get { return _baseType.AssemblyQualifiedName; }
        }

        public override Type BaseType
        {
            get { return _baseType.BaseType; }
        }

        public override string FullName
        {
            get { return _baseType.FullName; }
        }

        public override Guid GUID
        {
            get { return _baseType.GUID; }
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {

            throw new NotImplementedException();
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return _baseType.GetConstructors(bindingAttr);
        }

        public override Type GetElementType()
        {
            return _baseType.GetElementType();
        }

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return _baseType.GetEvent(name, bindingAttr);
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return _baseType.GetEvents(bindingAttr);
        }

        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return _baseType.GetField(name, bindingAttr);
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return _baseType.GetFields(bindingAttr);
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            return _baseType.GetInterface(name, ignoreCase);
        }

        public override Type[] GetInterfaces()
        {
            return _baseType.GetInterfaces();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return _baseType.GetMembers(bindingAttr);
        }

        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return _baseType.GetMethods(bindingAttr);
        }

        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return _baseType.GetNestedType(name, bindingAttr);
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return _baseType.GetNestedTypes(bindingAttr);
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            var properties = new List<PropertyInfo>();

            foreach (var key in _target.Keys)
                properties.Add(new CustomPropertyInfo(key, _target[key].GetType()));

            return properties.ToArray();
        }

        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            var propertyInfo = new CustomPropertyInfo(name, returnType);

            return propertyInfo;
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
        {
            return _baseType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            return _baseType.IsPrimitive;
        }

        public override Module Module
        {
            get { return _baseType.Module; }
        }

        public override string Namespace
        {
            get { return _baseType.Namespace; }
        }

        public override Type UnderlyingSystemType
        {
            get { return _baseType.UnderlyingSystemType; }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _baseType.GetCustomAttributes(attributeType, inherit);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return _baseType.GetCustomAttributes(inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return _baseType.IsDefined(attributeType, inherit);
        }

        public override string Name
        {
            get { return _baseType.Name; }
        }
    }
}
