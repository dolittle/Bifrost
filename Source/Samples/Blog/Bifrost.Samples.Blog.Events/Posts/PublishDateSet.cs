using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class PublishDateSet : Event
    {
        public PublishDateSet(Guid id, DateTime dateTime) : base(id)
        {
            PublishDate = dateTime;
        }

        public DateTime PublishDate { get; set; }
    }
}
