using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Web
{
    public class DoStuffCommandHandler : ICommandHandler
    {
        IAggregatedRootRepository<TheObject> _aggregatedRootRepository;

        public DoStuffCommandHandler(IAggregatedRootRepository<TheObject> aggregatedRootRepository)
        {
            _aggregatedRootRepository = aggregatedRootRepository;
        }

        public void Handle(DoStuffCommand doStuff)
        {
            var theObject = _aggregatedRootRepository.Get(doStuff.Id);
            theObject.DoStuff(doStuff.StringParameter);
        }
    }
}