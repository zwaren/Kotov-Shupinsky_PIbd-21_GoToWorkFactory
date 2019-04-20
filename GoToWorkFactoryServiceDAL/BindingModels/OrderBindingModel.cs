using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
	public class OrderBindingModel
	{
		public int Id { get; set; }

		public int ClientId { get; set; }

		public decimal Sum { get; set; }

		public bool Reserved { get; set; }
		
		public List<OrderProductBindingModel> OrderProducts { get; set; }
	}
}
