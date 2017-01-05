/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="ICallContext"/>
    /// </summary>
    public class DefaultCallContext : ICallContext
    {
        [ThreadStatic]
        static Dictionary<string, object> _data;

        static Dictionary<string, object> Data => _data ?? (_data = new Dictionary<string, object>());

#pragma warning disable 1591 // Xml Comments
        public bool HasData(string key)
        {
            return Data.ContainsKey(key);
        }

        public T GetData<T>(string key)
        {
            return (T)Data[key];
        }

        public void SetData(string key, object data)
        {
            Data[key] = data;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
