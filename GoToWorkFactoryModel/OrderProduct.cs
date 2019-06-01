using System.Runtime.Serialization;

namespace GoToWorkFactoryModel
{
    [DataContract]
    public class OrderProduct
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public virtual Order Order { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }
	}
}
