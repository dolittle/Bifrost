using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Serialization;

namespace Bifrost.WCF.Events
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
