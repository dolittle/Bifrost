using System;
using Bifrost.Samples.Blog.Domain.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post
{
    public class when_creating_a_post_with_an_empty_title
    {
        static Post post;
        static Guid expected_blog_guid;
        static Exception exception;

        Because of = () =>
                         {
                             post = new Post(Guid.NewGuid());
                             expected_blog_guid = Guid.NewGuid();
                             exception = Catch.Exception(() => post.Create(expected_blog_guid, string.Empty));
                         };

        It should_throw_an_argument_exception = () => exception.ShouldBeOfType<Exception>();


    }
}