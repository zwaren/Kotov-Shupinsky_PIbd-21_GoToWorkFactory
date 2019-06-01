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
        
        public virtual Order Order { get; set; }
        
        public virtual Product Product { get; set; }
	}
}
