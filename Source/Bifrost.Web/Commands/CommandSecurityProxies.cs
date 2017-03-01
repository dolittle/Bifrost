﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Text;
using Bifrost.CodeGeneration;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Web.Configuration;
using Bifrost.Web.Proxies;

namespace Bifrost.Web.Commands
{
    public class CommandSecurityProxies : IProxyGenerator
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ICodeGenerator _codeGenerator;
        readonly ICommandSecurityManager _commandSecurityManager;
        readonly WebConfiguration _configuration;

        public CommandSecurityProxies(
            ITypeDiscoverer typeDiscoverer,
            ICodeGenerator codeGenerator,
            ICommandSecurityManager commandSecurityManager,
            WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
            _commandSecurityManager = commandSecurityManager;
        }

        string ClientNamespace(string @namespace)
        {
            return _configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace) ?? Namespaces.COMMANDS;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer
                .FindMultiple<ICommand>()
                .Where(t => !t.IsGenericType)
                .Where(t => !CommandProxies.NamespacesToExclude.Any(t.Namespace.StartsWith))
                .OrderBy(t => t.FullName)
                .GroupBy(t => ClientNamespace(t.Namespace))
                .OrderBy(n => n.Key);
            var result = new StringBuilder();

            foreach (var @namespace in typesByNamespace)
            {
                var currentNamespace = _codeGenerator.Namespace(@namespace.Key);
                foreach (var type in @namespace)
                {
                    var command = Activator.CreateInstance(type) as ICommand;
                    var authorizationResult = _commandSecurityManager.Authorize(command);
                    var name = $"{type.Name.ToCamelCase()}SecurityContext";
                    currentNamespace.Content.Assign(name)
                        .WithType(t => t
                            .WithSuper("Bifrost.commands.CommandSecurityContext")
                            .Function
                            .Body
                            .Variant("self", v => v.WithThis())
                            .Access("this", a => a
                                .WithFunctionCall(f => f
                                    .WithName("isAuthorized")
                                    .WithParameters(authorizationResult.IsAuthorized.ToString().ToCamelCase())
                                )
                            )
                        );
                }

                result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }

            return result.ToString();
        }
    }
}
