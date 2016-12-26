using System.Linq;
using Bifrost.Read;
using Bifrost.Sagas;

namespace Web.Read.HumanResources.Employees
{
    public class AllEmployees : IQueryFor<Employee>
    {
        IReadModelRepositoryFor<Employee> _repository;
        public AllEmployees(IReadModelRepositoryFor<Employee> repository, ISagaNarrator sagaNarrator)
        {
            _repository = repository;
        }

        public IQueryable<Employee> Query { get { return _repository.Query; } }
    }
}
