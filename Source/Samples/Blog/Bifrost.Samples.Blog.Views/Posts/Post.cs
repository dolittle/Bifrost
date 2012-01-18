using System;
using System.Collections.Generic;
using Bifrost.Views;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class Post : IHaveId
    {
        public Post()
        {
            UpdateDate = SystemClock.GetCurrentTime();
            PublishDate = SystemClock.GetCurrentTime(); ;
        }

        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime UpdateDate { get; set; }

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get { return _publishDate; }
            set
            {
                _publishDate = value;
                YearOfPublication = _publishDate.Year;
                MonthOfPublication = _publishDate.Month;
            }
        }

        public int YearOfPublication { get; set; }
        public int MonthOfPublication { get; set; }

        public IEnumerable<PostAttachment> Attachments { get; set; }
        public IEnumerable<PostTag> Tags { get; set; }
    }
}