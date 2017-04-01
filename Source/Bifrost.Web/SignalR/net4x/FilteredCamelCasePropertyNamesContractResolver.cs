/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Bifrost.Extensions;

namespace Bifrost.Web.SignalR
{
    public class FilteredCamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if( !member.DeclaringType.Assembly.FullName.Contains("Microsoft") )
                jsonProperty.PropertyName = jsonProperty.PropertyName.ToCamelCase();

            return jsonProperty;
        }
    }
}
