﻿using Microsoft.AspNet.SignalR;

namespace Web.HumanResources.Employees
{
    public class TestHub : Hub
    {
        public TestHub()
        {
        }

        public int GetSomething(string par1, int par2)
        {
            Clients.All.doSomething(par1, par2);
            return 42;
        }
    }
}