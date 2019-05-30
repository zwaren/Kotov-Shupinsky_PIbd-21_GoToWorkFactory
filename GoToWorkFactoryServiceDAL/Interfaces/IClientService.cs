using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
	public interface IClientService
	{
		List<ClientViewModel> GetList();
		ClientViewModel GetElement(int id);
        ClientViewModel GetElement(ClientBindingModel model);
        void AddElement(ClientBindingModel model);
		void UpdElement(ClientBindingModel model);
		void DelElement(int id);
	}
}
