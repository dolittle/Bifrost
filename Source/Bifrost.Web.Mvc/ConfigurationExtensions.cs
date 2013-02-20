using System.Web.Mvc;
using Bifrost.Validation;
using Bifrost.Web.Mvc;
using Bifrost.Web.Mvc.Views;
using FluentValidation.Mvc;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingKnockout(this IConfigure configuration)
        {
            ViewEngines.Engines.Add(new KnockoutRazorViewEngine());
            return Configure.Instance;
        }

        public static IConfigure ConfigureDependencyResolver(this IConfigure configure)
        {
            var dependencyResolver = new ContainerDependencyResolver(configure.Container);
            DependencyResolver.SetResolver(dependencyResolver);
            return configure;
        }

        public static IConfigure UsingFluentValidation(this IConfigure configure)
        {
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            var factory = configure.Container.Get<CommandValidatorFactory>();
            FluentValidationModelValidatorProvider.Configure(p =>
            {
                p.ValidatorFactory = factory;
                p.AddImplicitRequiredValidator = false;
            });

            var validatorFactory = configure.Container.Get<DefaultValidatorFactory>();
            var validatorProvider = new FluentValidationModelValidatorProvider(validatorFactory);
            validatorProvider.AddImplicitRequiredValidator = false;
            ModelValidatorProviders.Providers.Add(validatorProvider);

            return configure;
        }
    }
}
