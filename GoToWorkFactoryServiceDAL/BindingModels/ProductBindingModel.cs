using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
	public class ProductBindingModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public List<ProductMaterialBindingModel> ProductMaterials { get; set; }
	}
}
