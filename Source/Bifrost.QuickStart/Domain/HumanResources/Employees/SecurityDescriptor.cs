﻿using Bifrost.Commands;
using Bifrost.Security;

namespace Web.Domain.HumanResources.Employees
{
    public class SecurityDescriptor : BaseSecurityDescriptor
    {
        public SecurityDescriptor()
        {
            //When.Handling().Commands().InstanceOf<RegisterEmployee>(s => s.User().MustBeInRole("Create"));
        }
    }
}