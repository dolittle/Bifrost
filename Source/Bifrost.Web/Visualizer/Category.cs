using Bifrost.Read;

namespace Bifrost.Web.Visualizer
{
    public class Category : IReadModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
