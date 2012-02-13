using System.Collections.Generic;
using System.Linq;
using Bifrost.Views;

namespace Bifrost.Web
{
    public class StuffToPersistService
    {
        IView<StuffToPersist> _view;

        public StuffToPersistService(IView<StuffToPersist> view)
        {
            _view = view;
        }

        public IEnumerable<StuffToPersist> GetAll()
        {
            return _view.Query.ToArray();
        }
    }
}