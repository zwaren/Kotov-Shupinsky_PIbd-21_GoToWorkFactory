using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class ProductViewModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ProductMaterialViewModel> ProductMaterials { get; set; }
	}
}
