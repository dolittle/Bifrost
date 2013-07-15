using Bifrost.Configuration;
using FluentValidation;

namespace Bifrost.Validation
{
    /// <summary>
    /// Configures Validation
    /// </summary>
    public class Configurator : ICanConfigure
    {
        /// <summary>
        /// Instantiates the Configurator for Validation
        /// </summary>
        /// <param name="configure"></param>
        public void Configure(IConfigure configure)
        {
            ValidatorOptions.DisplayNameResolver = NameResolvers.DisplayNameResolver;
            ValidatorOptions.PropertyNameResolver = NameResolvers.PropertyNameResolver;
        }
    }
}