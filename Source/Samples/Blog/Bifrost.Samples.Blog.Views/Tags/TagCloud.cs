using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Samples.Blog.Views.Tags
{
    public class TagCloud
    {
        public Dictionary<Tag, int> TagCountsByTag { get; set; }

        public Tag[] Tags
        {
            get { return TagCountsByTag.Keys.ToArray(); }
        }
    }
}
