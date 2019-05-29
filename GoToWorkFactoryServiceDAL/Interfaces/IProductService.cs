using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
	public interface IProductService
	{
		List<ProductViewModel> GetList();
		List<ProductViewModel> GetFilteredList();
        ProductViewModel GetElement(int id);
		void AddElement(ProductBindingModel model);
		void UpdElement(ProductBindingModel model);
		void DelElement(int id);
	}
}
