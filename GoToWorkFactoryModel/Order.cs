using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkFactoryModel
{
	public class Order
    {
		public int Id { get; set; }

		public int ClientId { get; set; }

		public decimal Sum { get; set; }

		public bool Reserved { get; set; }

		public OrderStatus Status { get; set; }

		public DateTime DateCreate { get; set; }

		public DateTime? DateImplement { get; set; }

		public virtual Client Client { get; set; }

		[ForeignKey("OrderId")]
		public virtual List<OrderProduct> OrderProducts { get; set; }
	}
}
