using System;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public interface IPostStatisticsService
    {
        void AggregateForMonth(int year, int month,  params Guid[] postsToExclude);
    }
}