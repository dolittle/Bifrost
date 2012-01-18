using System;
using Bifrost.MSpec.Events;
using Bifrost.Samples.Blog.Domain.Posts;
using Bifrost.Samples.Blog.Domain.Posts.Commands;
using Bifrost.Samples.Blog.Events.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post_command_handlers
{

    public class when_handling_create_post : given.a_post_command_handler
    {
        static Post post;
        static CreatePost create_post;
        static Guid post_id;

        Establish context = () =>
                                {
                                    post_id = Guid.NewGuid();
                                    post = new Post(post_id);
                                    factory_mock.Setup(f => f.Create(post_id)).Returns(post);
                                };

        Because of = () =>
                         {
                             create_post = new CreatePost
                                               {
                                                   Id = post_id,
                                                   Title = "A Title",
                                                   Body = "A Body",
                                                   BlogId = Guid.NewGuid(),
                                               };
                             command_handler.Handle(create_post);
                         };

        It should_create_a_post = () => factory_mock.Verify(f => f.Create(Moq.It.IsAny<Guid>()));
        It should_apply_post_created_with_values = () =>
                                                   post.ShouldHaveEvent<PostCreated>().AtBeginning().
                                                       Where(c => c.Title.ShouldEqual(create_post.Title),
                                                              c => c.BlogId.ShouldEqual(create_post.BlogId));
        It should_apply_title_set_with_values = () =>
                                                post.ShouldHaveEvent<TitleSet>().AtSequenceNumber(1).
                                                    Where(c => c.Title.ShouldEqual(create_post.Title));
        It should_apply_body_set_with_values = () =>
                                        post.ShouldHaveEvent<BodySet>().AtEnd().
                                            Where(c => c.Body.ShouldEqual(create_post.Body));

    }
}
