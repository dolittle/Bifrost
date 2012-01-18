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
using System.Collections.Generic;
using Bifrost.Events;

namespace Bifrost.Configuration.Xml
{
	/// <summary>
	/// Represents a <see cref="IConfigElement"/> for configuring <see cref="IEvent">events</see> from Xml
	/// </summary>
    public class EventsElement : IConfigElement
    {
		/// <summary>
		/// Gets or sets the storage configuration to use
		/// </summary>
        public StorageElement Storage { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="IEventRepository"/> type to use
		/// </summary>
        public Type RepositoryType { get; set; }
    }
}