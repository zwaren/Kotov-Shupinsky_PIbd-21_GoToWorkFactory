namespace GoToWorkFactoryServiceDAL.BindingModels
{
	public class OrderProductBindingModel
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int Count { get; set; }
	}
}
