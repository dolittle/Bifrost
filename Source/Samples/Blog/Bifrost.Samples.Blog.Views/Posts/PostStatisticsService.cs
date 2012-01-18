using System;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostStatisticsService : IPostStatisticsService
    {
        private readonly IEntityContext<Post> _postsEntityContext;
        private readonly IEntityContext<PostsByYear> _postByYearEntityContext;

        public PostStatisticsService(IEntityContext<Post> postsEntityContext, IEntityContext<PostsByYear> postByYearEntityContext)
        {
            _postsEntityContext = postsEntityContext;
            _postByYearEntityContext = postByYearEntityContext;
        }

        public void AggregateForMonth(int year, int month, params Guid[] postsToExclude)
        {
            Month targetMonth = (Month) month;

            var existing = _postByYearEntityContext.Entities.Where(pby => pby.Year == year).FirstOrDefault();

            PostsByYear postsByYear = existing ?? new PostsByYear() {Year = year};

            PopulatePostCountsPerMonth(postsByYear, targetMonth, postsToExclude);

            if (existing == null)
            {
                _postByYearEntityContext.Insert(postsByYear);
            }
            else
            {
                _postByYearEntityContext.Update(postsByYear);
            }
        }

        private void PopulatePostCountsPerMonth(PostsByYear postsByYear, Month targetMonth,  Guid[] postsToExclude)
        {
            var monthlyPostCounts = from posts in _postsEntityContext.Entities
                                    where posts.YearOfPublication == postsByYear.Year
                                    group posts by posts.MonthOfPublication
                                    into stats
                                    select new {Month = (Month) stats.Key, Count = stats.Count()};

            foreach(var monthlyPostCount in monthlyPostCounts )
            {
                postsByYear.SetMonthCount(monthlyPostCount.Month,monthlyPostCount.Count);
            }

            postsByYear.IncrementPostCountForMonth(targetMonth);
        }


    }
}