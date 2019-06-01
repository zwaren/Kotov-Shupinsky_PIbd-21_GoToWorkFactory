using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GoToWorkFactoryModel
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
		public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
		public virtual List<OrderProduct> OrderProducts { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
		public virtual List<ProductMaterial> ProductMaterials { get; set; }
	}
}
