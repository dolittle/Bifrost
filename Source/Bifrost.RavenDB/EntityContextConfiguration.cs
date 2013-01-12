using System;
using Bifrost.Configuration;
using Bifrost.Entities;
using System.Net;

namespace Bifrost.RavenDB
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
        public ICredentials Credentials { get; set; }
        public Type EntityContextType { get { return typeof(EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
    }
}
