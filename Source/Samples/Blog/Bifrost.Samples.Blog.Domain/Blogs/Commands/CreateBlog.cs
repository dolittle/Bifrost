using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Blogs.Commands
{
    public class CreateBlog : Command
    {
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Owner { get; set; }
    }
}
