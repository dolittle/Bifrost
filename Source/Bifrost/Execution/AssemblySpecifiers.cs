/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Configuration.Assemblies;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblySpecifiers"/>
    /// </summary>
    public class AssemblySpecifiers : IAssemblySpecifiers
    {
        static readonly object LockObject = new object();

        readonly IAssembliesConfiguration _assembliesConfiguration;
        readonly ISet<string> _specifiedAssemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblySpecifiers"/>
        /// </summary>
        public AssemblySpecifiers(IAssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
            _specifiedAssemblies = new HashSet<string>();
        }

#pragma warning disable 1591 // Xml Comments
        public bool SpecifyUsingSpecifiersFrom(Assembly assembly)
        {
            lock (LockObject)
            {
                if (_specifiedAssemblies.Contains(assembly.FullName))
                {
                    return false;
                }

                _specifiedAssemblies.Add(assembly.FullName);

                var specified = false;
                if (MayReferenceICanSpecifyAssemblies(assembly))
                {
                    try
                    {
                        specified = assembly
                            .GetTypes()
                            .Where(t => t.GetTypeInfo().GetInterfaces().Contains(typeof(ICanSpecifyAssemblies)))
                            .Select(SpecifyFrom)
                            .ToList()
                            .Count > 0;
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        throw new AssemblySpecificationException(
                            $"Error while reflecting on the types of {assembly.FullName}. Loader exceptions encountered:\n" +
                            string.Join("\n", e.LoaderExceptions.Select(l => l.Message).Distinct()));
                    }
                }

                return specified;
            }
        }

        ICanSpecifyAssemblies SpecifyFrom(Type type)
        {
            try
            {
                var specifier = Activator.CreateInstance(type) as ICanSpecifyAssemblies;
                specifier.Specify(_assembliesConfiguration);
                return specifier;
            }
            catch (MissingMethodException)
            {
                throw new AssemblySpecificationException(
                    $"Could not create instance of type {type.FullName}. It must have a default constructor.");
            }
            catch (Exception e)
            {
                throw new AssemblySpecificationException(
                    $"Error while specifying assemblies from {type.FullName}: {e.Message}",
                    e);
            }
        }

        static bool MayReferenceICanSpecifyAssemblies(Assembly assembly)
        {
            return
                assembly.FullName.Contains("Bifrost") ||
                assembly.GetReferencedAssemblies().Any(a => a.FullName.Contains("Bifrost"));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
