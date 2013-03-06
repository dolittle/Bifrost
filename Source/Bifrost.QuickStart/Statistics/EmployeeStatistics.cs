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
        public void Record(CommandResult commandResult, IVisitableStatistic statistic)
        {
            if (commandResult.CommandName.ToLower() == "registeremployee")
            {
                statistic.Record("RegisteredEmployee");
            }
        }
    }
}