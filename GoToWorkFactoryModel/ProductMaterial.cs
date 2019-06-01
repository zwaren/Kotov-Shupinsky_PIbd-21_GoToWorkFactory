using System.Runtime.Serialization;

namespace GoToWorkFactoryModel
{
    [DataContract]
    public class ProductMaterial
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int MaterialId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public virtual Material Material { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }
	}
}
