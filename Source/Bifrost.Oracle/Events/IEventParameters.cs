using System.Collections.Generic;
using Bifrost.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public interface IEventParameters
    {
        EventParameter[] Parameters { get; }
        EventMetaData GetMetaDataFor(IEvent @event);
        IEnumerable<OracleParameter> BuildFromEvent(int position, IEvent @event);
    }
}