﻿using Bifrost.Specs.Commands.for_CommandHandlerInvoker;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class DummyStatisticsPlugin : IStatisticsPlugin
    {
        readonly IList<string> _categories;
        public DummyStatisticsPlugin()
        {
            _categories = new List<string>();
        }

        public string Context
        {
            get { return "DummyStatisticsPluginContext"; }
        }

        public ICollection<string> Categories
        {
            get { return _categories; }
        }

        public bool WasHandled(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a was handled statistic");
            return true;
        }

        public bool HadException(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a had exception statistic");
            return true;
        }

        public bool HadValidationError(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a had validation error statistic");
            return true;
        }

        public bool DidNotPassSecurity(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a did not pass security statistic");
            return true;
        }
    }

    public class DummyStatisticsPluginWithNoEffect : IStatisticsPlugin
    {
        readonly IList<string> _categories;
        public DummyStatisticsPluginWithNoEffect()
        {
            _categories = new List<string>();
        }

        public string Context
        {
            get { return "DummyStatisticsPluginContext"; }
        }

        public ICollection<string> Categories
        {
            get { return _categories; }
        }

        public bool WasHandled(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a was handled statistic");
            return false;
        }

        public bool HadException(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a had exception statistic");
            return false;
        }

        public bool HadValidationError(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a had validation error statistic");
            return false;
        }

        public bool DidNotPassSecurity(Bifrost.Commands.Command command)
        {
            _categories.Add("I touched a did not pass security statistic");
            return false;
        }
    }
}
