using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GoToWorkFactoryModel
{
    [DataContract]
    public class Order
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
        public OrderStatus Status { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateImplement { get; set; }

        [DataMember]
        public virtual Client Client { get; set; }

		[ForeignKey("OrderId")]
		public virtual List<OrderProduct> OrderProducts { get; set; }
	}
}
