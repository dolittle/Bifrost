using Bifrost.Security;
using Bifrost.Read;

namespace Web.Read.HumanResources.Employees
{
    public class QuerySecurity : BaseSecurityDescriptor
    {
        public QuerySecurity()
        {
            /*
            When
                .Fetching()
                    .ReadModels()
                        .InNamespace(
                            typeof(Employee).Namespace, 
                            s => s.User()
                                    .MustBeInRole("ReadAccess"));*/
        }

    }
}