using System;
using System.Collections.Generic;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostsByYear
    {
        readonly IDictionary<Month, int> _counts;

        public PostsByYear()
        {
            _counts = new Dictionary<Month, int>()
                           {
                               {Month.January, 0},
                               {Month.February, 0},
                               {Month.March, 0},
                               {Month.April, 0},
                               {Month.May, 0},
                               {Month.June, 0},
                               {Month.July, 0},
                               {Month.August, 0},
                               {Month.September, 0},
                               {Month.October, 0},
                               {Month.November, 0},
                               {Month.December, 0}
                           };
        }

        public Guid BlogId { get; set; }
        public int Year { get; set; }
        public int January
        {
            get { return GetMonthCount(Month.January); }
            set { SetMonthCount(Month.January,value); }
        }
        public int February
        {
            get { return GetMonthCount(Month.February); }
            set { SetMonthCount(Month.February, value); }
        }
        public int March
        {
            get { return GetMonthCount(Month.March); }
            set { SetMonthCount(Month.March, value); }
        }
        public int April
        {
            get { return GetMonthCount(Month.April); }
            set { SetMonthCount(Month.April, value); }
        }
        public int May
        {
            get { return GetMonthCount(Month.May); }
            set { SetMonthCount(Month.May, value); }
        }
        public int June
        {
            get { return GetMonthCount(Month.June); }
            set { SetMonthCount(Month.June, value); }
        }
        public int July
        {
            get { return GetMonthCount(Month.July); }
            set { SetMonthCount(Month.July, value); }
        }
        public int August
        {
            get { return GetMonthCount(Month.August); }
            set { SetMonthCount(Month.August, value); }
        }
        public int September
        {
            get { return GetMonthCount(Month.September); }
            set { SetMonthCount(Month.September, value); }
        }
        public int October
        {
            get { return GetMonthCount(Month.October); }
            set { SetMonthCount(Month.October, value); }
        }
        public int November
        {
            get { return GetMonthCount(Month.November); }
            set { SetMonthCount(Month.November, value); }
        }
        public int December
        {
            get { return GetMonthCount(Month.December); }
            set { SetMonthCount(Month.December, value); }
        }

        public int GetMonthCount(Month month)
        {
            return _counts[month];
        }

        public void SetMonthCount(Month month, int value)
        {
            _counts[month] = value;
        }

        public void IncrementPostCountForMonth(Month month)
        {
            var monthCount = GetMonthCount(month);
            SetMonthCount(month,++monthCount);
        }

        public IDictionary<Month,int> Counts
        {
            get { return _counts; }
        }
    }
}
