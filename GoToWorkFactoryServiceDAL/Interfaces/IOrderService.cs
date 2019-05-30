using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.ViewModels;
using System.Collections.Generic;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
    public interface IOrderService
    {
        List<OrderViewModel> GetList();
        OrderViewModel GetElement(int id);
        void FinishOrder(OrderBindingModel model);
        void DelElement(int id);
    }
}
