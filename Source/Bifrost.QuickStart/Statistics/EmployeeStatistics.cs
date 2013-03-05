using Bifrost.Commands;
using Bifrost.QuickStart.Domain.HumanResources.Employees;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bifrost.QuickStart.Statistics
{
    public class EmployeeStatistics : ICanRecordStatisticsForCommand
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

        public bool Record(CommandResult commandResult)
        {
            if (commandResult.CommandName.ToLower() == "RegisterEmployee")
            {
                Categories.Add("RegisteredEmployee");
                return true; 
            }

            return false;
        }
    }
}