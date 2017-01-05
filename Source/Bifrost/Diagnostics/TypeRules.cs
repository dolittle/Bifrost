/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Execution;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeRules"/>
    /// </summary>
    public class TypeRules : ITypeRules
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IProblemsFactory _problemsFactory;
        IProblemsReporter _problemsReporter;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeRules"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> used for discovering rules</param>
        /// <param name="container"><see cref="IContainer"/> used for getting instances</param>
        /// <param name="problemsFactory"><see cref="IProblemsFactory"/> used for creating problems</param>
        /// <param name="problemsReporter"><see cref="IProblemsReporter">Reporter</see> to use for reporting back any problems</param>
        public TypeRules(
                    ITypeDiscoverer typeDiscoverer, 
                    IContainer container, 
                    IProblemsFactory problemsFactory, 
                    IProblemsReporter problemsReporter)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _problemsFactory = problemsFactory;
            _problemsReporter = problemsReporter;
        }

#pragma warning disable 1591 // Xml Comments
        public void ValidateAll()
        {
            var ruleTypes = _typeDiscoverer.FindMultiple(typeof(ITypeRuleFor<>));
            foreach (var ruleType in ruleTypes)
            {
                var rule = (dynamic)_container.Get(ruleType);

                var typeForRule = ruleType.GetTypeInfo().GetInterface(typeof(ITypeRuleFor<>).Name).GetTypeInfo().GetGenericArguments()[0];
                var types = _typeDiscoverer.FindMultiple(typeForRule);
                foreach (var type in types)
                {
                    var problems = _problemsFactory.Create();
                    rule.Validate(type, problems);

                    if (problems.Any)
                        _problemsReporter.Report(problems);
                }
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
