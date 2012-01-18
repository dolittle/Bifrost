using System;
using Bifrost.MSpec.Events;
using Bifrost.Samples.Blog.Domain.Posts;
using Bifrost.Samples.Blog.Events.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post
{
    public class when_creating_a_post_with_a_valid_title_and_blog
    {
        static Post post;
        static string expected_title;
        static Guid expected_blog_guid;

        Because of = () =>
                         {
                             post = new Post(Guid.NewGuid());
                             expected_blog_guid = Guid.NewGuid();
                             expected_title = "Something";
                             post.Create(expected_blog_guid, expected_title);
                         };

        It should_apply_post_created_event = () => post.ShouldHaveEvent<PostCreated>().AtBeginning();
        It should_apply_post_created_with_title = () => post.ShouldHaveEvent<PostCreated>().AtBeginning().Where(c => c.Title.ShouldEqual(expected_title));
        It should_apply_post_created_with_blog = () => post.ShouldHaveEvent<PostCreated>().AtBeginning().Where(c => c.BlogId.ShouldEqual(expected_blog_guid));
    }
}
