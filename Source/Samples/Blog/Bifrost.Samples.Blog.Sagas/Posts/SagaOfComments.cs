using Bifrost.Sagas;

namespace Bifrost.Samples.Blog.Sagas.Posts
{
	public class SagaOfComments : Saga
	{
		public static string PartitionName = typeof (SagaOfComments).Name;

		public SagaOfComments()
		{
			Partition = PartitionName;
		}
	}
}