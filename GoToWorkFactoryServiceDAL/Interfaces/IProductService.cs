using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
	public interface IProductService
	{
		List<ProductViewModel> GetList();
		ProductViewModel GetElement(int id);
		void AddElement(ProductBindingModel model);
		void UpdElement(ProductBindingModel model);
		void DelElement(int id);
	}
}
