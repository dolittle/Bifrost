using System;

namespace Web.Domain.HumanResources.Foos
{
    public class SecuredService
    {
        public string SecuredAction()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}