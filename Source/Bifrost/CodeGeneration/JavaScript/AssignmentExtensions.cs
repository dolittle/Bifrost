/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with assignment
    /// </summary>
    public static class AssignmentExtensions
    {
        /// <summary>
        /// Assign a key within an <see cref="ObjectLiteral"/>
        /// </summary>
        /// <param name="objectLiteral"><see cref="ObjectLiteral"/> to assign to</param>
        /// <param name="name">Name of key</param>
        /// <returns><see cref="KeyAssignment"/> to build</returns>
        public static KeyAssignment Assign(this ObjectLiteral objectLiteral, string name)
        {
            var keyAssignment = new KeyAssignment(name);
            objectLiteral.AddChild(keyAssignment);
            return keyAssignment;
        }

        /// <summary>
        /// Assign "this" to an <see cref="Assignment"/>
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithThis(this Assignment assignment)
        {
            assignment.Value = new This();
            return assignment;
        }

        /// <summary>
        /// Assign a literal
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="literal">Literal to assign</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithLiteral(this Assignment assignment, object literal)
        {
            assignment.Value = new Literal(literal);
            return assignment;
        }

        /// <summary>
        /// Assign a string
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="theString">String to assign</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithString(this Assignment assignment, string theString)
        {
            assignment.WithLiteral(string.Format("\"{0}\"", theString));
            return assignment;
        }


        /// <summary>
        /// Assign a type
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{TypeExtension}"/> that gets called to build the type</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithType(this Assignment assignment, Action<TypeExtension> callback)
        {
            var typeExtension = new TypeExtension();
            assignment.Value = typeExtension;
            callback(typeExtension);
            return assignment;
        }

        /// <summary>
        /// Assign a function
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{Function}"/> that gets called to build the function</param>
        /// <param name="parameters">Optional parameters for the function</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithFunction(this Assignment assignment, Action<Function> callback, params string[] parameters)
        {
            var function = new Function(parameters);
            assignment.Value = function;
            callback(function);
            return assignment;
        }

        /// <summary>
        /// Assign a function call
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{FunctionCall}"/> that gets called to build the functioncall</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithFunctionCall(this Assignment assignment, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            assignment.Value = functionCall;
            callback(functionCall);
            return assignment;
        }

        /// <summary>
        /// Assign an observable
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="visitor">Optional <see cref="Action{Observable}"/> that gets called to build the observable</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObservable(this Assignment assignment, ObservableVisitor visitor = null)
        {
            var observable = new Observable();
            assignment.Value = observable;

            if (visitor != null) visitor(assignment.Name, observable);

            return assignment;
        }

        /// <summary>
        /// Assign the default value of a given Type
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="type">The type of which to create a default for</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithDefaultValue(this Assignment assignment, Type type)
        {
            if( type == typeof(Guid) )
                return assignment.WithGuidEmpty();
            else if (type.GetTypeInfo().IsValueType)
            {
                if (type.IsNumericType())
                    return assignment.WithDefaultNumericValue(type);

                if (type.IsDate())
                    return assignment.WithDate();

                if (type.IsBoolean())
                    return assignment.WithBoolean();
            }
            else
            {
                if( type == typeof(string) )return assignment.WithLiteral("\"\"");
                return assignment.WithNullValue();
            }
            return assignment;
        }

        /// <summary>
        /// Assign an empty guid value
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithGuidEmpty(this Assignment assignment)
        {
            return assignment.WithLiteral("Bifrost.Guid.empty");
        }

        /// <summary>
        /// Assign null value
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithNullValue(this Assignment assignment)
        {
            assignment.Value = new Null();
            return assignment;
        }

        /// <summary>
        /// Assign new Date value
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithDate(this Assignment assignment)
        {
            assignment.Value = new Date();
            return assignment;
        }

        /// <summary>
        /// Assign bool value
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="value"><see cref="bool"/> to assign</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithBoolean(this Assignment assignment, bool value = false)
        {
            assignment.Value = new Boolean(value);
            return assignment;
        }

        /// <summary>
        /// Assign the default value of a given enum type
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="enumType">The type of enum which to create a default for</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithDefaultEnumValue(this Assignment assignment, Type enumType)
        {
            if (enumType == null)
                throw new ArgumentException("Type cannot be null");

            var defaultValue = Activator.CreateInstance(enumType);
            assignment.WithLiteral((int)defaultValue);

            return assignment;
        }
        
        /// <summary>
        /// Assign the default value of a given Type
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="type">The type of which to create a default for</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithDefaultNumericValue(this Assignment assignment, Type type)
        {
            if(type == null)
                throw new ArgumentException("Type cannot be null");

            if(!type.IsNumericType())
                throw new ArgumentException(string.Concat("Type must be a numeric type.  Type is: ",type.ToString()));

            var defaultValue = Activator.CreateInstance(type);
            assignment.WithLiteral(string.Format("{0}", defaultValue));
            
            return assignment;
        }


        /// <summary>
        /// Assign an observable array
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObservableArray(this Assignment assignment)
        {
            return assignment.WithFunctionCall(f => f.WithName("ko.observableArray"));
        }

        /// <summary>
        /// Assign an object literal
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <param name="callback"><see cref="Action{ObjectLiteral}"/> that gets called to build the object literal</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithObjectLiteral(this Assignment assignment, Action<ObjectLiteral> callback = null)
        {
            var objectLiteral = new ObjectLiteral();
            if (callback != null) callback(objectLiteral);
            assignment.Value = objectLiteral;
            return assignment;
        }


        /// <summary>
        /// Assign an empty Array
        /// </summary>
        /// <param name="assignment"><see cref="Assignment"/> to assign to</param>
        /// <returns>The <see cref="Assignment"/> to build on</returns>
        public static Assignment WithEmptyArray(this Assignment assignment)
        {
            assignment.WithLiteral(string.Format("[]"));
            return assignment;
        }
    }
}
