/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Read
{
    public static class ReadModelProxyExtensions
    {
        public static Container WithReadModelConvenienceFunctions(this FunctionBody functionBody, Type type)
        {
            var excludePropertiesFrom = typeof(IReadModel);
            var properties = type.GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

            foreach (var property in properties)
            {
                var functionName = string.Format("matching{0}",property.Name.ToPascalCase());
                var propertyName = property.Name.ToCamelCase();
                var filter = new ObjectLiteral();
                filter.Assign(propertyName).WithLiteral(propertyName);

                functionBody.Property(functionName,p =>
                    p.WithFunction(function => 
                        function
                            .WithParameters(propertyName)
                                .Body
                                    .Scope("self", scope=>
                                        scope.FunctionCall(f=>f.WithName("instanceMatching").WithParameters(new[] { filter })
                                    )
                                )
                            )
                        );
            }

            return functionBody;
        }
    }
}
