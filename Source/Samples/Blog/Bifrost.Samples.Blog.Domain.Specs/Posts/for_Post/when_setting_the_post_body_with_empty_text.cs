using System;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post
{
    public class when_setting_the_post_body_with_empty_text : given.a_created_post
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => Post.SetBody(string.Empty));

        private It should_throw_an_exception = () => exception.ShouldBeOfType<Exception>();
    }
}