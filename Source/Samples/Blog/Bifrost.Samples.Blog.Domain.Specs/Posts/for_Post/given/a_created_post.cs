using System;
using Bifrost.Samples.Blog.Domain.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post.given
{
    public class a_created_post
    {
        protected static Guid BlogId;
        protected static string Title;
        protected static Post Post;
        protected static Guid PostId;

        private Establish context = () =>
                                        {
                                            PostId = Guid.NewGuid();
                                            BlogId = Guid.NewGuid();
                                            Title = "A valid post title";
                                            Post = new Post(PostId);
                                            Post.Create(BlogId, Title);

                                            Post.Commit();
                                        };
    }
}