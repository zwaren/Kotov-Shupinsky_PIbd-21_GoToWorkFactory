using GoToWorkFactoryServiceDAL.BindingModels;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
    public interface IClientMainService
	{
        void CreateOrder(OrderBindingModel model);
    }
}
