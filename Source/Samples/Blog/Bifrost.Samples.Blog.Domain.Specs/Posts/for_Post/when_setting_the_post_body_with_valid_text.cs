using Bifrost.Samples.Blog.Events.Posts;
using Machine.Specifications;
using Bifrost.MSpec.Events;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post
{
    public class when_setting_the_post_body_with_valid_text : given.a_created_post
    {
        static string BodyText = "This is valid text for a post body";

        Because of = () => Post.SetBody(BodyText);

        It should_apply_body_set_event = () => Post.ShouldHaveEvent<BodySet>().AtBeginning();
        It should_apply_body_set_with_body_text = () => Post.ShouldHaveEvent<BodySet>().AtBeginning().Where(c => c.Body.ShouldEqual(BodyText));
    }
}