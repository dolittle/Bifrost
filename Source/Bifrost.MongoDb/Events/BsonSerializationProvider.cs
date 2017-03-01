/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDb.Events
{
    public class BsonSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (typeof(MethodInfo).IsAssignableFrom(type))
                return new MethodInfoSerializer();

            if (typeof(Type).IsAssignableFrom(type))
                return new TypeSerializer();

            return null;
        }
    }
}
