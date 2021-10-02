namespace WebStore.ViewModels
{
	public class BrandViewModel
	{
		#region For frontend fields

		public int Id { get; set; }

		public int Order { get; set; }

		#endregion

		public string Name { get; set; }

		public int PositionsAmount { get; set; }
	}
}
