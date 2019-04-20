using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public List<ProductMaterialViewModel> ProductMaterials { get; set; }
	}
}
