using The_Gifters.Models.Entities;

namespace The_Gifters.Models
{
	public class ParticipateService
	{
		private readonly GiftersContext giftersContext;

		public ParticipateService(GiftersContext giftersContext)
		{
			this.giftersContext = giftersContext;
		}
	}
}
