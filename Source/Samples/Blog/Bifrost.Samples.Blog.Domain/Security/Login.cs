using System;
using Bifrost.Domain;

namespace Bifrost.Samples.Blog.Domain.Security
{
    public class Login : AggregatedRoot
    {
        public Login(Guid id) : base(id) {}
    }
}
