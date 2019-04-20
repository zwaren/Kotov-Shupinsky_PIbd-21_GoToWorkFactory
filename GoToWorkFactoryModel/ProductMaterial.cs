namespace GoToWorkFactoryModel
{
	public class ProductMaterial
	{
		public int Id { get; set; }

		public int ProductId { get; set; }

		public int MaterialId { get; set; }

		public int Count { get; set; }

		public virtual Material Material { get; set; }

		public virtual Product Product { get; set; }
	}
}
