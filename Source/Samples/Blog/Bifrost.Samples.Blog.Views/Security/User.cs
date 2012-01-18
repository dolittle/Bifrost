using System;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Security
{
    public class User : IHaveId
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
