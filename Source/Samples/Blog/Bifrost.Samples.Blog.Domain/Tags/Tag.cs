using System;
using Bifrost.Domain;
using Bifrost.Samples.Blog.Events.Tags;

namespace Bifrost.Samples.Blog.Domain.Tags
{
    public class Tag : AggregatedRoot
    {
        public Tag(Guid id) : base(id)
        {
        }

        public void Create()
        {
            Apply(new TagCreated(Id));
        }

        public void SetName(string name)
        {
            Apply(new TagNameSet(Id, name));
        }

        public void Delete()
        {
            Apply(new TagDeleted(Id));
        }

        public void Handle(TagCreated created)
        {
            
        }

        public void Handle(TagNameSet tagNameSet)
        {
        }

        public void Handle(TagDeleted tagDeleted)
        {
            
        }
    }
}
