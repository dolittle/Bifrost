using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Bifrost.Ninject
{
    public class NinjectServiceBehaviorExtension : BehaviorExtensionElement
    {
        private readonly ConfigurationPropertyCollection _configurationProperties = new ConfigurationPropertyCollection();

        protected override object CreateBehavior()
        {
            var behavior = new NinjectServiceBehavior();
            return behavior;
        }

        public override Type BehaviorType
        {
            get { return typeof (NinjectServiceBehavior); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _configurationProperties; }
        }
    }
}