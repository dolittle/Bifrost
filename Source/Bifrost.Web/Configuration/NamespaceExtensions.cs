/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Configuration
{
    public static class NamespaceExtensions
    {
        public static FunctionBody WithNamespaceMappersFrom(this FunctionBody global, PathToNamespaceMappers namespaceMappers)
        {
            foreach( var map in namespaceMappers.Maps ) {
                global.Access("namespaceMapper",
                    a => a.WithFunctionCall(
                        f => f.WithName("addMapping").WithParameters("\"" + map.Key + "\"", "\"" + map.Value + "\"")));
            }

            return global;
        }
    }
}
