using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Security.Commands
{
    public class CreateUser : Command
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}