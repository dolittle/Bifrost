using System;

namespace Bifrost.QuickStart.Domain.HumanResources.Foos
{
    public class SecuredService
    {
        public string SecuredAction()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}