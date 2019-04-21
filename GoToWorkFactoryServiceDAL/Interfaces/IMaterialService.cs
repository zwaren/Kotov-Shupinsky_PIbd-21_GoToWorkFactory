using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
	public interface IMaterialService
	{
		List<MaterialViewModel> GetList();
		MaterialViewModel GetElement(int id);
		void AddElement(MaterialBindingModel model);
		void UpdElement(MaterialBindingModel model);
		void DelElement(int id);
	}
}
