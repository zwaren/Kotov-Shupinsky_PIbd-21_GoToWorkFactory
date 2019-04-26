using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    [DataContract]
	public class OrderViewModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public string ClientName { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public bool Reserved { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string DateImplement { get; set; }

        [DataMember]
        public List<OrderProductViewModel> OrderProducts { get; set; }
	}
}
