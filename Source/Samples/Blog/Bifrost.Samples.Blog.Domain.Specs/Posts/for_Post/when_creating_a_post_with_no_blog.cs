using System;
using Bifrost.Samples.Blog.Domain.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post
{
    public class when_creating_a_post_with_no_blog
    {
        static Post post;
        static string expected_title;
        static Exception exception;

        Because of = () =>
                         {
                             post = new Post(Guid.NewGuid());
                             expected_title = "A valid post title";
                             exception = Catch.Exception(() => post.Create( Guid.Empty, expected_title));
                         };

        It should_throw_an_argument_exception = () => exception.ShouldBeOfType<Exception>();
    }
}