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
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Serialization;

namespace Bifrost.Services.Events
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EventService
    {
        IEntityContext<EventHolder> _entityContext;
        ISerializer _serializer;

        public EventService(IEntityContext<EventHolder> entityContext, ISerializer serializer)
        {
            _entityContext = entityContext;
            _serializer = serializer;
        }

        [WebGet]
        public IEnumerable<EventHolder> GetAll()
        {
            return _entityContext.Entities.ToArray();
        }

        [WebGet]
        public string GetAllAsJsonString()
        {
            var events = GetAll();
            var jsonString = _serializer.ToJson(events);
            return jsonString;
        }
    }
}
