using System.Linq;
using Bifrost.Read;

namespace Bifrost.QuickStart.Read.HumanResources.Employees
{
    public class AllEmployees : IQueryFor<Employee>
    {
        IReadModelRepositoryFor<Employee> _repository;
        public AllEmployees(IReadModelRepositoryFor<Employee> repository)
        {
            _repository = repository;
        }

        public IQueryable<Employee> Query { get { return _repository.Query; } }
    }
}
