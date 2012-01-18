using Bifrost.Commands;

namespace Bifrost.Samples.Blog.Domain.Security.Commands
{
    public class SetPassword : Command
    {
        public string Password { get; set; }
    }
}