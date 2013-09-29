using System.Linq;
using Bifrost.Read;

namespace Bifrost.Web.Visualizer
{
    public class Categories : IQueryFor<Category>
    {

        public IQueryable<Category> Query
        {
            get
            {
                return new[] {
                    new Category { 
                        Name = "QualityAssurance",
                        DisplayName = "Quality Assurance",
                        Description = "System Code Quality"

                    }
                }.AsQueryable();
            }
        }
    }
}
