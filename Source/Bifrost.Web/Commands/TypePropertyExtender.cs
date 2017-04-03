/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.Web.Commands
{
    public class TypePropertyExtender : ICanExtendCommandProperty
    {
        public void Extend(Type commandType, string propertyName, Observable observable)
        {
            var property = commandType.GetTypeInfo().GetProperty(propertyName.ToPascalCase());
            if (property != null)
            {
                var type = property.PropertyType;
                if (type.IsConcept()) type = type.GetConceptValueType();

                var clientType = string.Empty;

                if (type == typeof(byte) ||
                    type == typeof(Int16) ||
                    type == typeof(Int32) ||
                    type == typeof(Int64) ||
                    type == typeof(UInt16) ||
                    type == typeof(UInt32) ||
                    type == typeof(UInt64) ||
                    type == typeof(float) ||
                    type == typeof(double) ||
                    type == typeof(decimal) )
                {
                    clientType = "Number";
                }
                else if (type == typeof(DateTime))
                {
                    clientType = "Date";
                }
                else if (type == typeof(string))
                {
                    clientType = "String";
                }

                if (!string.IsNullOrEmpty(clientType)) observable.ExtendWith("type", string.Format("\"{0}\"",clientType));
            }
        }
    }
}