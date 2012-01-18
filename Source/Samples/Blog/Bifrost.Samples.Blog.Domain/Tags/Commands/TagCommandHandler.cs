using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Samples.Blog.Domain.Tags.Commands
{
    public class TagCommandHandler : ICommandHandler
    {
        readonly IAggregatedRootFactory<Tag> _factory;
        readonly IAggregatedRootRepository<Tag> _repository;

        public TagCommandHandler(IAggregatedRootFactory<Tag> factory, IAggregatedRootRepository<Tag> repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public void Handle(CreateTag createTag)
        {
            var tag = _factory.Create(createTag.Id);
            tag.Create();
            tag.SetName(createTag.TagName);
        }

        public void Handle(DeleteTag deleteTag)
        {
            var tag = _repository.Get(deleteTag.Id);
            tag.Delete();
        }
    }
}
