namespace GoToWorkFactoryServiceDAL.BindingModels
{
	public class ProductMaterialBindingModel
	{
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int MaterialId { get; set; }

		public int MaterialName { get; set; }

		public int Count { get; set; }
	}
}
