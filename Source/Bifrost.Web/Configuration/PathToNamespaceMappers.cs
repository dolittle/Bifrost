/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using Bifrost.Configuration;

namespace Bifrost.Web.Configuration
{
    public class PathToNamespaceMappers
    {
        List<KeyValuePair<string, string>> _maps = new List<KeyValuePair<string, string>>();

        public IEnumerable<KeyValuePair<string, string>> Maps { get { return _maps; } }

        public PathToNamespaceMappers()
        {
            AddDefaults();
        }

        public void Clear()
        {
            _maps.Clear();
        }

        public void AddDefaults()
        {
            var baseNamespace = Configure.Instance.EntryAssembly.GetName().Name;
            var @namespace = string.Format("{0}.**.",baseNamespace);
            Add("**/", @namespace);
            Add("/**/", @namespace);
            Add("", baseNamespace);
        }

        public void Add(string format, string mappedFormat)
        {
            _maps.Add(new KeyValuePair<string, string>(format, mappedFormat));
        }
    }
}
