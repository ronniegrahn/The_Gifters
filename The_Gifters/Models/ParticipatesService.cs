using The_Gifters.Models.Entities;

namespace The_Gifters.Models
{
	public class ParticipatesService
	{
		private readonly GiftersContext giftersContext;

		public ParticipatesService(GiftersContext giftersContext)
		{
			this.giftersContext = giftersContext;
		}

	}
}
