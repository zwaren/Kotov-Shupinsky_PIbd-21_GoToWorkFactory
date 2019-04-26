using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class OrderProductBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Count { get; set; }
	}
}
