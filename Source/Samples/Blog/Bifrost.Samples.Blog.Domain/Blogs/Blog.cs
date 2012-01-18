using System;
using Bifrost.Domain;
using Bifrost.Samples.Blog.Events.Blogs;

namespace Bifrost.Samples.Blog.Domain.Blogs
{
    public class Blog : AggregatedRoot
    {
        public Blog(Guid id) : base(id)
        {
            
        }

        public void Create()
        {
            Apply(new BlogCreated(Id));
        }

        public void SetName(string name)
        {
            Apply(new BlogNameSet(Id, name));
        }

        public void SetTagLine(string tagLine)
        {
            Apply(new BlogTagLineSet(Id, tagLine));
        }

        public void AssignOwner(string owner)
        {
            Apply(new BlogOwnerAssigned(Id, owner));
        }


        public void Handle(BlogCreated blogCreated) {}
        public void Handle(BlogNameSet blogNameSet) {}
        public void Handle(BlogTagLineSet blogTagLineSet) {}
        public void Handle(BlogOwnerAssigned blogOwnerAssigned) {}
    }
}
