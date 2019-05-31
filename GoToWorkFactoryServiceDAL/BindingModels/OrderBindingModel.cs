using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class OrderBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public bool Reserved { get; set; }

        [DataMember]
        public List<OrderProductBindingModel> OrderProducts { get; set; }
    }
}
