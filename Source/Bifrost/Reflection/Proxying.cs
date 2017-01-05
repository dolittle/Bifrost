/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using System.Reflection.Emit;

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
            var assemblyName = new AssemblyName(dynamicAssemblyName);
            DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            DynamicModule = DynamicAssembly.DefineDynamicModule(dynamicModuleName);
        }

#pragma warning disable 1591 // Xml Comments
        public Type BuildInterfaceWithPropertiesFrom(Type type)
        {
            var typeBuilder = DefineInterface(type);

            foreach (var property in type.GetTypeInfo().GetProperties())
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType, new Type[0]);
                var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new Type[0]);
                propertyBuilder.SetGetMethod(getMethodBuilder);
                var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new[] { property.PropertyType });
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            var interfaceForType = typeBuilder.CreateTypeInfo().AsType();
            return interfaceForType;
        }


        public Type BuildClassWithPropertiesFrom(Type type)
        {
            var typeBuilder = DefineClass(type);

            foreach (var property in type.GetTypeInfo().GetProperties())
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType, new Type[0]);
                var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Virtual, property.PropertyType, new Type[0]);
                propertyBuilder.SetGetMethod(getMethodBuilder);
                var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Virtual, property.PropertyType, new[] { property.PropertyType });
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            var classForType = typeBuilder.CreateTypeInfo().AsType();
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
