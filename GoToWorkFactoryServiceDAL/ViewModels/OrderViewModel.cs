using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		public int ClientId { get; set; }

		public string ClientName { get; set; }

		public decimal Sum { get; set; }

		public bool Reserved { get; set; }

		public string Status { get; set; }

		public string DateCreate { get; set; }

		public string DateImplement { get; set; }
		
		public List<OrderProductViewModel> OrderProducts { get; set; }
	}
}
