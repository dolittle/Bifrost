using Bifrost.Specs.Commands.for_CommandHandlerInvoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : ICommandStatistics
    {
        public void WasHandled(Bifrost.Commands.Command command)
        {
            throw new NotImplementedException();
        }
    }
}
