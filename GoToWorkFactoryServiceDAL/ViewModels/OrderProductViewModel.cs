using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class OrderProductViewModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Count { get; set; }
	}
}
