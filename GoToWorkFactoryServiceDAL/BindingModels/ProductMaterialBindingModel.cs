using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class ProductMaterialBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int MaterialId { get; set; }

        [DataMember]
        public int MaterialName { get; set; }

        [DataMember]
        public int Count { get; set; }
	}
}
