#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Xml.Serialization;

namespace Bifrost.Notification
{
	public class NotifyingObjectWeaver
	{
		private const string DynamicAssemblyName = "Dynamic Assembly";
		private const string DynamicModuleName = "Dynamic Module";
		private const string PropertyChangedEventName = "PropertyChanged";
		private const string OnPropertyChangedMethodName = "OnPropertyChanged";
		private const string DispatcherFieldName = "Dispatcher";
		private const string DispatcherManagerCurrentPropertyName = "Current";

		private static readonly Type VoidType = typeof(void);
		private static readonly Type DelegateType = typeof(Delegate);

		private const MethodAttributes EventMethodAttributes =
			MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;

		private const MethodAttributes OnPropertyChangedMethodAttributes =
			MethodAttributes.Public | MethodAttributes.HideBySig;


		private static readonly AssemblyBuilder DynamicAssembly;
		private static readonly ModuleBuilder DynamicModule;

		private static readonly Dictionary<Type, Type> Proxies = new Dictionary<Type, Type>();

		static NotifyingObjectWeaver()
		{
			var dynamicAssemblyName = CreateUniqueName(DynamicAssemblyName);
			var dynamicModuleName = CreateUniqueName(DynamicModuleName);
			var appDomain = Thread.GetDomain();
			var assemblyName = new AssemblyName(dynamicAssemblyName);
			DynamicAssembly = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			DynamicModule = DynamicAssembly.DefineDynamicModule(dynamicModuleName, true);
		}

		public static void ClearTypeCache()
		{
			Proxies.Clear();
		}

		public Type GetProxyType<T>()
		{
			var type = typeof(T);
			var proxyType = GetProxyType(type);
			return proxyType;
		}

		public Type GetProxyType(Type type)
		{

			Type proxyType;
			if (Proxies.ContainsKey(type))
			{
				proxyType = Proxies[type];
			}
			else
			{
				proxyType = CreateProxyType(type);
				Proxies[type] = proxyType;
			}

			return proxyType;
		}

		private static Type CreateProxyType(Type type)
		{
			var typeBuilder = DefineType(type);
			var eventHandlerType = typeof(PropertyChangedEventHandler);

			AddAttributesToType(type,typeBuilder);

			var propertyChangedFieldBuilder = typeBuilder.DefineField(PropertyChangedEventName, eventHandlerType, FieldAttributes.Private);
			var dispatcherFieldBuilder = typeBuilder.DefineField(DispatcherFieldName, typeof(IDispatcher),
																 FieldAttributes.Private | FieldAttributes.Static);

			

			DefineTypeInitializer(type, typeBuilder, dispatcherFieldBuilder);
			DefineConstructorIfNoDefaultConstructorOnBaseType(type, typeBuilder);
			OverrideToStringIfNotOverridenInBaseType(type, typeBuilder);

			DefineEvent(typeBuilder, eventHandlerType, propertyChangedFieldBuilder);
			var onPropertyChangedMethodBuilder = DefineOnPropertyChangedMethod(typeBuilder, eventHandlerType, propertyChangedFieldBuilder, dispatcherFieldBuilder);


			DefineProperties(typeBuilder, type, onPropertyChangedMethodBuilder);

			var proxyType = typeBuilder.CreateType();
			return proxyType;
		}

		private static void AddAttributesToType(Type type, TypeBuilder typeBuilder)
		{
			AddAttributeToType<XmlRootAttribute>(typeBuilder,
				new Dictionary<string, object>() { { "ElementName", type.Name } });
			/*
			AddAttributeToType<DataContractAttribute>(typeBuilder,
				new Dictionary<string, object>() { { "Name", type.Name } });*/
		}

		private static void AddAttributeToType<T>(TypeBuilder typeBuilder, IDictionary<string, object> propertiesWithValues)
			where T : Attribute
		{
			var attributeType = typeof (T);
			var constructor = attributeType.GetConstructor(new Type[0]);

			var properties = new List<PropertyInfo>();
			var values = new List<object>();

			foreach (var propertyName in propertiesWithValues.Keys)
			{
				var property = attributeType.GetProperty(propertyName);
				properties.Add(property);
				values.Add(propertiesWithValues[propertyName]);
			}

			var attributeBuilder =
				new CustomAttributeBuilder(
					constructor,
					new object[0],
					properties.ToArray(),
					values.ToArray());

			typeBuilder.SetCustomAttribute(attributeBuilder);
		}


		private static void OverrideToStringIfNotOverridenInBaseType(Type type, TypeBuilder typeBuilder)
		{
			var toStringMethod = type.GetMethod("ToString");
			if ((toStringMethod.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask)
			{
				var fullName = type.FullName;

				var newToStringMethod = typeBuilder.DefineMethod("ToString", toStringMethod.Attributes^MethodAttributes.VtableLayoutMask, typeof(string),
																new Type[0]);

				var toStringBuilder = newToStringMethod.GetILGenerator();
				toStringBuilder.DeclareLocal(typeof (string));
				toStringBuilder.Emit(OpCodes.Nop);
				toStringBuilder.Emit(OpCodes.Ldstr,fullName);
				toStringBuilder.Emit(OpCodes.Stloc_0);
				toStringBuilder.Emit(OpCodes.Ldloc_0);
				toStringBuilder.Emit(OpCodes.Ret);

				typeBuilder.DefineMethodOverride(newToStringMethod,toStringMethod);
			}
		}


		private static void DefineTypeInitializer(Type type, TypeBuilder typeBuilder, FieldBuilder dispatcherFieldBuilder)
		{
			var constructorBuilder = typeBuilder.DefineTypeInitializer();
			var constructorGenerator = constructorBuilder.GetILGenerator();

			var dispatcherManagerType = typeof(DispatcherManager);
			var currentGetCurrentMethodName = string.Format("get_{0}", DispatcherManagerCurrentPropertyName);
			var dispatcherGetCurrentMethod = dispatcherManagerType.GetMethod(currentGetCurrentMethodName);

			constructorGenerator.Emit(OpCodes.Call, dispatcherGetCurrentMethod);
			constructorGenerator.Emit(OpCodes.Stsfld, dispatcherFieldBuilder);
			constructorGenerator.Emit(OpCodes.Ret);
		}


		private static void DefineConstructorIfNoDefaultConstructorOnBaseType(Type type, TypeBuilder typeBuilder)
		{
			var constructors = type.GetConstructors();
			if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
			{
				DefineDefaultConstructor(type, typeBuilder);
				return;
			}

			foreach (var constructor in constructors)
			{
				var parameters = constructor.GetParameters().Select(p => p.ParameterType).ToArray();
				var constructorBuilder = typeBuilder.DefineConstructor(constructor.Attributes, constructor.CallingConvention, parameters);
				var constructorGenerator = constructorBuilder.GetILGenerator();
				constructorGenerator.Emit(OpCodes.Ldarg_0);

				for (var index = 0; index < parameters.Length; index++)
				{
					constructorGenerator.Emit(OpCodes.Ldarg, index + 1);
				}
				constructorGenerator.Emit(OpCodes.Call, constructor);
				constructorGenerator.Emit(OpCodes.Nop);
				constructorGenerator.Emit(OpCodes.Nop);
				constructorGenerator.Emit(OpCodes.Nop);
				constructorGenerator.Emit(OpCodes.Ret);
			}
		}

		private static void DefineDefaultConstructor(Type type, TypeBuilder typeBuilder)
		{
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
		}

		private static void DefineProperties(TypeBuilder typeBuilder, Type baseType, MethodBuilder onPropertyChangedMethodBuilder)
		{
			var properties = baseType.GetProperties();
			var query = from p in properties
						where p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal
						select p;

			var virtualProperties = query.ToArray();
			foreach (var property in virtualProperties)
			{

				if (ShouldPropertyBeIgnored(property))
				{
					continue;
				}
				DefineGetMethodForProperty(property, typeBuilder);
				DefineSetMethodForProperty(property, typeBuilder, onPropertyChangedMethodBuilder);
			}
		}

		private static bool ShouldPropertyBeIgnored(PropertyInfo propertyInfo)
		{
			var attributes = propertyInfo.GetCustomAttributes(typeof(IgnoreChangesAttribute), true);
			return attributes.Length == 1;
		}

		private static void DefineSetMethodForProperty(PropertyInfo property, TypeBuilder typeBuilder, MethodBuilder onPropertyChangedMethodBuilder)
		{
			var setMethodToOverride = property.GetSetMethod();
			if (null == setMethodToOverride)
			{
				return;
			}
			var setMethodBuilder = typeBuilder.DefineMethod(setMethodToOverride.Name, setMethodToOverride.Attributes, VoidType, new[] { property.PropertyType });
			var setMethodGenerator = setMethodBuilder.GetILGenerator();
			var propertiesToNotifyFor = GetPropertiesToNotifyFor(property);


			setMethodGenerator.Emit(OpCodes.Nop);
			setMethodGenerator.Emit(OpCodes.Ldarg_0);
			setMethodGenerator.Emit(OpCodes.Ldarg_1);
			setMethodGenerator.Emit(OpCodes.Call, setMethodToOverride);

			foreach (var propertyName in propertiesToNotifyFor)
			{
				setMethodGenerator.Emit(OpCodes.Ldarg_0);
				setMethodGenerator.Emit(OpCodes.Ldstr, propertyName);
				setMethodGenerator.Emit(OpCodes.Call, onPropertyChangedMethodBuilder);
			}

			setMethodGenerator.Emit(OpCodes.Nop);
			setMethodGenerator.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(setMethodBuilder, setMethodToOverride);
		}

		private static string[] GetPropertiesToNotifyFor(PropertyInfo property)
		{
			var properties = new List<string>();
			properties.Add(property.Name);

			var attributes = property.GetCustomAttributes(typeof(NotifyChangesForAttribute), true);
			foreach (NotifyChangesForAttribute attribute in attributes)
			{
				foreach (var propertyName in attribute.PropertyNames)
				{
					properties.Add(propertyName);
				}
			}
			return properties.ToArray();
		}

		private static void DefineGetMethodForProperty(PropertyInfo property, TypeBuilder typeBuilder)
		{
			var getMethodToOverride = property.GetGetMethod();
			var getMethodBuilder = typeBuilder.DefineMethod(getMethodToOverride.Name, getMethodToOverride.Attributes, property.PropertyType, new Type[0]);
			var getMethodGenerator = getMethodBuilder.GetILGenerator();
			var label = getMethodGenerator.DefineLabel();

			getMethodGenerator.DeclareLocal(property.PropertyType);
			getMethodGenerator.Emit(OpCodes.Nop);
			getMethodGenerator.Emit(OpCodes.Ldarg_0);
			getMethodGenerator.Emit(OpCodes.Call, getMethodToOverride);
			getMethodGenerator.Emit(OpCodes.Stloc_0);
			getMethodGenerator.Emit(OpCodes.Br_S, label);
			getMethodGenerator.MarkLabel(label);
			getMethodGenerator.Emit(OpCodes.Ldloc_0);
			getMethodGenerator.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(getMethodBuilder, getMethodToOverride);
		}


		private static void DefineEvent(TypeBuilder typeBuilder, Type eventHandlerType, FieldBuilder fieldBuilder)
		{
			var eventBuilder = typeBuilder.DefineEvent("PropertyChanged", EventAttributes.None, eventHandlerType);
			DefineAddMethodForEvent(typeBuilder, eventHandlerType, fieldBuilder, eventBuilder);
			DefineRemoveMethodForEvent(typeBuilder, eventHandlerType, fieldBuilder, eventBuilder);
		}

		private static void DefineRemoveMethodForEvent(TypeBuilder typeBuilder, Type eventHandlerType, FieldBuilder fieldBuilder, EventBuilder eventBuilder)
		{
			var removeEventMethod = string.Format("remove_{0}", PropertyChangedEventName);
			var removeMethodInfo = DelegateType.GetMethod("Remove", BindingFlags.Public | BindingFlags.Static, null,
														  new[] { DelegateType, DelegateType }, null);
			var removeMethodBuilder = typeBuilder.DefineMethod(removeEventMethod, EventMethodAttributes, VoidType, new[] { eventHandlerType });
			var removeMethodGenerator = removeMethodBuilder.GetILGenerator();
			removeMethodGenerator.Emit(OpCodes.Ldarg_0);
			removeMethodGenerator.Emit(OpCodes.Ldarg_0);
			removeMethodGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
			removeMethodGenerator.Emit(OpCodes.Ldarg_1);
			removeMethodGenerator.EmitCall(OpCodes.Call, removeMethodInfo, null);
			removeMethodGenerator.Emit(OpCodes.Castclass, eventHandlerType);
			removeMethodGenerator.Emit(OpCodes.Stfld, fieldBuilder);
			removeMethodGenerator.Emit(OpCodes.Ret);
			eventBuilder.SetAddOnMethod(removeMethodBuilder);
		}

		private static void DefineAddMethodForEvent(TypeBuilder typeBuilder, Type eventHandlerType, FieldBuilder fieldBuilder, EventBuilder eventBuilder)
		{
			var combineMethodInfo = DelegateType.GetMethod("Combine", BindingFlags.Public | BindingFlags.Static, null,
														   new[] { DelegateType, DelegateType }, null);


			var addEventMethod = string.Format("add_{0}", PropertyChangedEventName);
			var addMethodBuilder = typeBuilder.DefineMethod(addEventMethod, EventMethodAttributes, VoidType, new[] { eventHandlerType });
			var addMethodGenerator = addMethodBuilder.GetILGenerator();
			addMethodGenerator.Emit(OpCodes.Ldarg_0);
			addMethodGenerator.Emit(OpCodes.Ldarg_0);
			addMethodGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
			addMethodGenerator.Emit(OpCodes.Ldarg_1);
			addMethodGenerator.EmitCall(OpCodes.Call, combineMethodInfo, null);
			addMethodGenerator.Emit(OpCodes.Castclass, eventHandlerType);
			addMethodGenerator.Emit(OpCodes.Stfld, fieldBuilder);
			addMethodGenerator.Emit(OpCodes.Ret);
			eventBuilder.SetAddOnMethod(addMethodBuilder);
		}

		private static MethodInfo GetMethodInfoFromType<T>(Expression<Action<T>> expression)
		{
			if (expression.Body is MethodCallExpression)
			{
				var methodCallExpresion = expression.Body as MethodCallExpression;
				return methodCallExpresion.Method;
			}
			return null;
		}

		private static MethodBuilder DefineOnPropertyChangedMethod(TypeBuilder typeBuilder, Type eventHandlerType, FieldBuilder propertyChangedFieldBuilder, FieldBuilder dispatcherFieldBuilder)
		{
			var propertyChangedEventArgsType = typeof(PropertyChangedEventArgs);

			var onPropertyChangedMethodBuilder = typeBuilder.DefineMethod(OnPropertyChangedMethodName, OnPropertyChangedMethodAttributes, VoidType,
																		  new[] { typeof(string) });
			var onPropertyChangedMethodGenerator = onPropertyChangedMethodBuilder.GetILGenerator();

			var checkAccessMethod = GetMethodInfoFromType<IDispatcher>(d => d.CheckAccess());
			var invokeMethod = GetMethodInfoFromType<PropertyChangedEventHandler>(e => e.Invoke(null, null));
			var beginInvokeMethod = GetMethodInfoFromType<IDispatcher>(d => d.BeginInvoke(null, null, null));

			var propertyChangedNullLabel = onPropertyChangedMethodGenerator.DefineLabel();
			var checkAccessFalseLabel = onPropertyChangedMethodGenerator.DefineLabel();
			var doneLabel = onPropertyChangedMethodGenerator.DefineLabel();

			onPropertyChangedMethodGenerator.DeclareLocal(typeof(PropertyChangedEventArgs));
			onPropertyChangedMethodGenerator.DeclareLocal(typeof(bool));
			onPropertyChangedMethodGenerator.DeclareLocal(typeof(object[]));
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);

			// if( null != PropertyChanged )
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldnull);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldfld, propertyChangedFieldBuilder);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ceq);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stloc_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Brtrue_S, propertyChangedNullLabel);

			// args = new PropertyChangedEventArgs()
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Newobj, propertyChangedEventArgsType.GetConstructor(new[] { typeof(string) }));
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stloc_0);

			// if( Dispatcher.CheckAccess() ) 
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldsfld, dispatcherFieldBuilder);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Callvirt, checkAccessMethod);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldc_I4_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ceq);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stloc_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Brtrue_S, checkAccessFalseLabel);

			// CheckAccess == true

			// Invoke
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldfld, propertyChangedFieldBuilder);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_0);
			onPropertyChangedMethodGenerator.EmitCall(OpCodes.Callvirt, invokeMethod, null);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Br_S, doneLabel);

			// CheckAccess == false
			onPropertyChangedMethodGenerator.MarkLabel(checkAccessFalseLabel);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldsfld, dispatcherFieldBuilder);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldfld, propertyChangedFieldBuilder);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldc_I4_2);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Newarr, typeof(object));
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stloc_2);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_2);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldc_I4_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldarg_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stelem_Ref);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_2);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldc_I4_1);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_0);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Stelem_Ref);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ldloc_2);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Callvirt, beginInvokeMethod);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);

			onPropertyChangedMethodGenerator.MarkLabel(propertyChangedNullLabel);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Nop);
			onPropertyChangedMethodGenerator.MarkLabel(doneLabel);
			onPropertyChangedMethodGenerator.Emit(OpCodes.Ret);
			return onPropertyChangedMethodBuilder;
		}

		private static TypeBuilder DefineType(Type type)
		{
			var name = CreateUniqueName(type.Name);
			var typeBuilder = DynamicModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

			AddInterfacesFromBaseType(type, typeBuilder);

			typeBuilder.SetParent(type);
			var interfaceType = typeof(INotifyPropertyChanged);
			typeBuilder.AddInterfaceImplementation(interfaceType);
			return typeBuilder;
		}

		private static void AddInterfacesFromBaseType(Type type, TypeBuilder typeBuilder)
		{
			var interfaces = type.GetInterfaces();
			foreach (var interfaceType in interfaces)
			{
				typeBuilder.AddInterfaceImplementation(interfaceType);
			}
		}

		private static string CreateUniqueName(string prefix)
		{
			var uid = Guid.NewGuid().ToString();
			uid = uid.Replace('-', '_');
			var name = string.Format("{0}{1}", prefix, uid);
			return name;
		}
	}
}
#endif