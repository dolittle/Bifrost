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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Bifrost.Execution;

namespace Bifrost.Configuration.Xml
{
    /// <summary>
    /// Defines a parser for Configuration information defined in Xml
    /// </summary>
    public class ConfigParser
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        Dictionary<string, Type> _availableConfigElements;

        /// <summary>
        /// Initializes an instance of <see cref="ConfigParser"/>
        /// </summary>
        /// <param name="typeDiscoverer">An instance of <see cref="ITypeDiscoverer"/></param>
        public ConfigParser(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
            InitializeConfigElements();
        }

        /// <summary>
        /// Parses the Xml and returns a strongly typed configuraiton object
        /// </summary>
        /// <typeparam name="T">Generic type indicating the concrete type of Config that is returned</typeparam>
        /// <param name="document">XDocument containing configuration information</param>
        /// <returns>An instance of the concrete configuration type</returns>
        public T Parse<T>(XDocument document) where T:new()
        {
            var config = new T();
            PopulateObject(config, document.Root);
            return config;
            
        }

        private void InitializeConfigElements()
        {
            _availableConfigElements = new Dictionary<string, Type>();
            var configElementTypes = _typeDiscoverer.FindMultiple<IConfigElement>();

            foreach (var configElementType in configElementTypes)
            {
                var name = configElementType.Name;
                var elementNameAttributes = configElementType.GetCustomAttributes(typeof(ElementNameAttribute), true);
                if (elementNameAttributes.Length == 1)
                    name = ((ElementNameAttribute)elementNameAttributes[0]).Name;

                _availableConfigElements[name] = configElementType;
            }
        }


        void PopulateObject(object configObject, XElement parentNode)
        {
            var configObjectType = configObject.GetType();
            var properties = configObjectType.GetProperties();
            foreach (var property in properties)
            {
                SetPropertyFromAttributeIfPossible(configObject, property, parentNode);

                var propertyName = property.Name;
                var node = GetNode(parentNode, propertyName);

                if (node == null) continue;

                object propertyValue = null;
                if ( IsValueOrStringType(property.PropertyType))
                {
                    propertyValue = ParseValueType(property.PropertyType, node.Value);
                }
                else
                {
                    if (IsIEnumerable(property.PropertyType))
                    {
                        propertyValue = CreateAndPopulateEnumerable(property.PropertyType, node);
                    }
                    else
                    {
                        XElement actualObjectNode;
                        var objectType = GetObjectTypeAndActualNode(property, node, out actualObjectNode);
                        propertyValue = CreateObjectFromXmlNode(objectType, actualObjectNode);
                    }
                }
                property.SetValue(configObject, propertyValue, null);
            }
        }

        object CreateAndPopulateEnumerable(Type type, XElement parentNode)
        {
            IList list;
            Type genericType = null;
            if( type.IsGenericType )
            {
                genericType = type.GetGenericArguments()[0];
                var genericListType = typeof (List<>).MakeGenericType(genericType);
                list = Activator.CreateInstance(genericListType) as IList;
            } else
            {
            	list = Array.CreateInstance(type,0);
            }

            foreach (var node in parentNode.Elements())
            {
                Type objectType = null;

                if( _availableConfigElements.ContainsKey(node.Name.LocalName))
                    objectType = _availableConfigElements[node.Name.LocalName];

                if (objectType == null && genericType != null)
                    objectType = genericType;

                if (objectType != null)
                {
                    var objectInstance = CreateObjectFromXmlNode(objectType, node);
                    list.Add(objectInstance);
                }
            }

            return list;

        }

        bool IsIEnumerable(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEnumerable)); ;
        }

        bool IsValueOrStringType(Type type)
        {
            return type.IsValueType || type == typeof (string);
        }

        XElement GetNode(XElement parentNode, string propertyName)
        {
            return parentNode.Elements()
                .Where(n => n.Name == propertyName)
                .Select(n => n).FirstOrDefault();
        }

        Type GetObjectTypeAndActualNode(PropertyInfo property, XElement node, out XElement actualObjectNode)
        {
			actualObjectNode = node.Elements().Count() == 0 ? node : node.Elements().First();

            ThrowIfConfigElementIsNotAvailable(node, property, actualObjectNode.Name.LocalName);

            Type objectType = null;
            if (!_availableConfigElements.ContainsKey(actualObjectNode.Name.LocalName))
            {
                actualObjectNode = node;
                objectType = property.PropertyType;
            }
            else
            {
                ThrowIfChildNodesIsEmpty(node);
                ThrowIfMoreThanOneChildNode(node);

                objectType = _availableConfigElements[actualObjectNode.Name.LocalName];
            }
            return objectType;
        }


        private object CreateObjectFromXmlNode(Type type, XElement node)
        {
            var instance = Activator.CreateInstance(type);
            PopulateObject(instance, node);
            return instance;
        }

        private void SetPropertyFromAttributeIfPossible(object instance, PropertyInfo property, XElement node)
        {
            foreach (var attribute in node.Attributes())
            {
                if (property.Name == attribute.Name)
                {
                    var propertyValue = ParseValueType(property.PropertyType, attribute.Value);
                    property.SetValue(instance, propertyValue, null);
                }
            }
        }

        private object ParseValueType(Type targetType, string value)
        {
            if (targetType == typeof(string))
                return value;

            if (targetType == typeof(int))
                return int.Parse(value);

            if (targetType == typeof(float))
                return float.Parse(value);

            if (targetType == typeof(double))
                return double.Parse(value);

            if (targetType == typeof(long))
                return long.Parse(value);

            if (targetType == typeof(Type))
                return Type.GetType(value);

            return null;
        }

        private void ThrowIfConfigElementIsNotAvailable(XElement node, PropertyInfo property, string name)
        {
            if (!_availableConfigElements.ContainsKey(name) && !_availableConfigElements.ContainsValue(property.PropertyType))
                throw new ArgumentException(string.Format("Element '{0}' is not a valid configuration element for '{1}'", name, node.Name));

        }

        private static void ThrowIfChildNodesIsEmpty(XElement node)
        {
            if (node.Elements().Count() == 0)
                throw new ArgumentException(string.Format("Configuration element '{0}' has no instance assigned to it", node.Name));
        }

        private static void ThrowIfMoreThanOneChildNode(XElement node)
        {
            if (node.Elements().Count() > 1)
                throw new ArgumentException(string.Format("Configuration element '{0}' has more than one instance assigned to it", node.Name));
        }
        
    }
}