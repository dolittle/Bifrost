using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    public class ContractToImplementorsMap : IContractToImplementorsMap
    {
        public void Feed(IEnumerable<Type> types)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> All
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Type> GetImplementorsFor<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetImplementorsFor(Type contract)
        {
            throw new NotImplementedException();
        }
    }
}
