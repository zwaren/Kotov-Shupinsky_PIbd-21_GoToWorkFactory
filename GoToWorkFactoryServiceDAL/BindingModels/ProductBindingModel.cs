using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class ProductBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ProductMaterialBindingModel> ProductMaterials { get; set; }
	}
}
