using Bifrost.QuickStart.Domain.HumanResources.Employees;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bifrost.QuickStart.Statistics
{
    public class EmployeeStatistics : IStatisticsPlugin
    {
        ICollection<string> _categories = new List<string>();

        public string Context
        {
            get { return "EmployeeStatistics"; }
        }

        public ICollection<string> Categories
        {
            get { return _categories; }
        }

        public bool WasHandled(Commands.ICommand command)
        {
            if (command.GetType() == typeof(RegisterEmployee))
            {
                var register = (RegisterEmployee)command;

                Categories.Add("RegisteredEmployee");

                if (string.IsNullOrEmpty(register.SocialSecurityNumber))
                    Categories.Add("RegisterEmployeeWithNoSocialSecurityNumber");

                return true; 
            }

            return false;
        }

        public bool HadException(Commands.ICommand command)
        {
            return false;
        }

        public bool HadValidationError(Commands.ICommand command)
        {
            return false;
        }

        public bool DidNotPassSecurity(Commands.ICommand command)
        {
            return false;
        }
    }
}