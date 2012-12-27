#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Entities;
using Bifrost.RavenDB;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingRavenDb(this IHaveStorage storage, string url, Action<EntityContextConfiguration> configureCallback = null) 
        {
            return UsingRavenDb(storage, Configure.Instance, url, configureCallback);
        }

        public static IConfigure UsingRavenDb(this IHaveStorage storage, IConfigure configure, string url, Action<EntityContextConfiguration> configureCallback = null)
        {
            var entityContextConfiguration = new EntityContextConfiguration
            {
                Url = url
            };
            if (configureCallback != null)
                configureCallback(entityContextConfiguration);

            var connection = new EntityContextConnection(entityContextConfiguration);
            entityContextConfiguration.Connection = connection;
            storage.EntityContextConfiguration = entityContextConfiguration;

            return configure;
        }

        public static void EventKeyGenerator<T>(this EntityContextConfiguration configuration) where T : ISequentialKeyGenerator
        {
            configuration.EventsKeyGeneratorType = typeof(T);
        }
    }
}
