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
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Represents an implementation of <see cref="IProxying"/>
    /// </summary>
    public class Proxying : IProxying
    {
        const string DynamicAssemblyName = "Dynamic Assembly";
        const string DynamicModuleName = "Dynamic Module";

        static readonly AssemblyBuilder DynamicAssembly;
        static readonly ModuleBuilder DynamicModule;

        static Proxying()
        {
            var dynamicAssemblyName = CreateUniqueName(DynamicAssemblyName);
            var dynamicModuleName = CreateUniqueName(DynamicModuleName);
            var appDomain = Thread.GetDomain();
            var assemblyName = new AssemblyName(dynamicAssemblyName);
            DynamicAssembly = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            DynamicModule = DynamicAssembly.DefineDynamicModule(dynamicModuleName, true);
        }

#pragma warning disable 1591 // Xml Comments
        public Type BuildInterfaceWithPropertiesFrom(Type type)
        {
            var typeBuilder = DefineInterface(type);

            foreach (var property in type.GetProperties())
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType, new Type[0]);
                var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new Type[0]);
                propertyBuilder.SetGetMethod(getMethodBuilder);
                var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new[] { property.PropertyType });
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            var interfaceForType = typeBuilder.CreateType();
            return interfaceForType;
        }


        public Type BuildClassWithPropertiesFrom(Type type)
        {
            var typeBuilder = DefineClass(type);

            foreach (var property in type.GetProperties())
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType, new Type[0]);
                var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Virtual, property.PropertyType, new Type[0]);
                propertyBuilder.SetGetMethod(getMethodBuilder);
                var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Virtual, property.PropertyType, new[] { property.PropertyType });
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            var classForType = typeBuilder.CreateType();
            return classForType;
        }

#pragma warning restore 1591 // Xml Comments

        static string CreateUniqueName(string prefix)
        {
            var uid = Guid.NewGuid().ToString();
            uid = uid.Replace('-', '_');
            var name = string.Format("{0}{1}", prefix, uid);
            return name;
        }


        static TypeBuilder DefineInterface(Type type)
        {
            var name = CreateUniqueName(type.Name);
            var typeBuilder = DynamicModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Serializable);
            return typeBuilder;
        }

        static TypeBuilder DefineClass(Type type)
        {
            var name = CreateUniqueName(type.Name);
            var typeBuilder = DynamicModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);
            return typeBuilder;
        }
    }
}
