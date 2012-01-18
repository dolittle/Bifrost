using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Samples.Blog.Domain.Blogs.Commands
{
    public class BlogCommandHandlers : ICommandHandler
    {
        readonly IAggregatedRootFactory<Blog> _factory;

        public BlogCommandHandlers(IAggregatedRootFactory<Blog> factory)
        {
            _factory = factory;
        }

        public void Handle(CreateBlog createBlog)
        {
            var blog = _factory.Create(createBlog.Id);
            blog.Create();
            blog.SetName(createBlog.Name);
            blog.SetTagLine(createBlog.TagLine);
            blog.AssignOwner(createBlog.Owner);
        }
    }
}