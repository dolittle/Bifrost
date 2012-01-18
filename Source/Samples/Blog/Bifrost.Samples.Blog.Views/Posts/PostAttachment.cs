using System;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostAttachment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}