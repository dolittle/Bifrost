using System;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Configuration
{
    public class DesktopConfiguration : IFrontendTargetConfiguration
    {
        public void Initialize(IContainer container)
        {
            container.Bind(typeof(ICommandFor<>), (Type t) =>
            {
                var commandForProxies = container.Get<ICommandForProxies>();
                return commandForProxies.CallGenericMethod<object, ICommandForProxies>(cc => cc.GetFor<Bifrost.Commands.Command>, t.GenericTypeArguments[0]);
            });
            
        }
    }
}
