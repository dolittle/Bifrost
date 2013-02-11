using System;
using System.Linq;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Web.Proxies.JavaScript;

namespace Bifrost.Web.Proxies
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
