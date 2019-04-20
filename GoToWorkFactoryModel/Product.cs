using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkFactoryModel
{
    public class Product
    {
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public decimal Price { get; set; }

		[ForeignKey("ProductId")]
		public virtual List<OrderProduct> OrderProducts { get; set; }

		[ForeignKey("ProductId")]
		public virtual List<ProductMaterial> ProductMaterials { get; set; }
	}
}
