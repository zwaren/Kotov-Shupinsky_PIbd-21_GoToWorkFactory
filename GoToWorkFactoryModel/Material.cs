using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkFactoryModel
{
	public class Material
    {
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int Count { get; set; }

		[ForeignKey("MaterialId")]
		public virtual List<ProductMaterial> ProductMaterials { get; set; }
	}
}
